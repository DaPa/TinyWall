﻿using System;
using System.Diagnostics;
using System.IO.Pipes;
using System.Security.Principal;
using System.Threading;
using TinyWall.Interface.Internal;


namespace PKSoft
{
    internal delegate TwMessage PipeDataReceived(TwMessage req);

    internal class PipeServerEndpoint : Disposable
    {
        private readonly Thread m_PipeWorkerThread;
        private readonly PipeDataReceived m_RcvCallback;
        private readonly string m_PipeName;

        private bool m_Run = true;
        private bool disposed = false;

        protected override void Dispose(bool disposing)
        {
            if (disposed)
                return;

            m_Run = false;

            // Create a dummy connection so that worker thread gets out of the infinite WaitForConnection()
            using (NamedPipeClientStream npcs = new NamedPipeClientStream(m_PipeName))
            {
                npcs.Connect(500);
            }

            if (disposing)
            {
                // Release managed resources
                m_PipeWorkerThread.Join(TimeSpan.FromMilliseconds(1000));
            }

            // Release unmanaged resources.
            // Set large fields to null.
            // Call Dispose on your base class.
            disposed = true;
            base.Dispose(disposing);
        }

        internal PipeServerEndpoint(PipeDataReceived recvCallback, string serverPipeName)
        {
            m_RcvCallback = recvCallback;
            m_PipeName = serverPipeName;

            m_PipeWorkerThread = new Thread(new ThreadStart(PipeServerWorker));
            m_PipeWorkerThread.IsBackground = true;
            m_PipeWorkerThread.Start();
        }

        private void PipeServerWorker()
        {
            // Allow authenticated users access to the pipe
            SecurityIdentifier AuthenticatedSID = new SecurityIdentifier(WellKnownSidType.AuthenticatedUserSid, null);
            PipeAccessRule par = new PipeAccessRule(AuthenticatedSID, PipeAccessRights.ReadWrite, System.Security.AccessControl.AccessControlType.Allow);
            PipeSecurity ps = new PipeSecurity();
            ps.AddAccessRule(par);

            while (m_Run)
            {
                try
                {
                    // Create pipe server
                    using (NamedPipeServerStream pipeServer = new NamedPipeServerStream(m_PipeName, PipeDirection.InOut, 1, PipeTransmissionMode.Message, PipeOptions.WriteThrough, 2048*10, 2048*10, ps))
                    {
                        if (!pipeServer.IsConnected)
                        {
                            pipeServer.WaitForConnection();
                            pipeServer.ReadMode = PipeTransmissionMode.Message;

                            if (!AuthAsServer(pipeServer))
                                throw new InvalidOperationException("Client authentication failed.");
                        }

                        // Read msg
                        TwMessage msg = SerializationHelper.DeserializeFromPipe<TwMessage>(pipeServer, 3000);

                        // Write response
                        TwMessage resp = m_RcvCallback(msg);
                        SerializationHelper.SerializeToPipe(pipeServer, resp);
                    } //using
                }
                catch { }
            } //while
        }

        private bool AuthAsServer(PipeStream stream)
        {
#if !DEBUG
            long clientPid;
            if (!Utils.SafeNativeMethods.GetNamedPipeClientProcessId(stream.SafePipeHandle.DangerousGetHandle(), out clientPid))
                return false;

            string clientFilePath = Utils.GetPathOfProcess((int)clientPid);

            return clientFilePath.Equals(ProcessManager.ExecutablePath, StringComparison.OrdinalIgnoreCase);
#else
            return true;
#endif
        }
    }
}
