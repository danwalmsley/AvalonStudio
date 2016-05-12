namespace AvalonStudio.Debugging.GDB
{
    public class TargetSelectCommand : Command<GDBResponse<string>>
    {
        public override int TimeoutMs
        {
            get
            {
                return DefaultCommandTimeout;
            }
        }

        public TargetSelectCommand(string host)
        {
            this.host = host;
        }

        private string host;

        public override string Encode()
        {
            return string.Format("-target-select remote {0}", host);
        }

        protected override GDBResponse<string> Decode(string response)
        {
            return new GDBResponse<string>(DecodeResponseCode(response));
        }

        public override void OutOfBandDataReceived(string data)
        {
            //throw new NotImplementedException ();
        }
    }
}
