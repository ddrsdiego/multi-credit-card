using System.Collections.Generic;
using System.Linq;

namespace MultiCreditCard.Application.Common
{
    public abstract class Response
    {
        private IList<string> _erros;

        protected Response()
        {
            _erros = new List<string>();
        }

        public bool HasError
        {
            get
            {
                return Errors.Any();
            }
        }

        public void AddError(string errorMessage)
        {
            if (!string.IsNullOrEmpty(errorMessage))
                _erros.Add(errorMessage);
        }

        public ICollection<string> Errors
        {
            get { return _erros; }
            private set { _erros = new List<string>(value); }
        }
    }
}
