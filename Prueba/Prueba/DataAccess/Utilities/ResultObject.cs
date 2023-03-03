namespace Prueba.DataAccess.Utilities
{
    public class ResultObject
    {
        public ResultObject()
        {
            this.Ok = true;
            IsAuthenticated = false;
        }

        public bool Ok { get; set; }

        public string Error { get; set; }

        public string TrazaError { get; set; }

        public Object Data { get; set; }

        public string ValidationUI { get; set; }

        public bool IsAuthenticated { get; set; }

        public string ResultValue { get; set; }
    }
}
