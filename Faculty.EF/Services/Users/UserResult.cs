using System;
using System.Collections.Generic;
using System.Text;
using Faculty.EFCore.Extensions;

namespace Faculty.EFCore.Services.Users
{
    public class UserResult
    {
        private readonly List<UserError> _errors = new List<UserError>();

        public bool Success { get; set; }
        public IEnumerable<UserError> Errors
        {
            get => _errors;
            set
            {
                _errors.Clear();
                if (value != null)
                {
                    AddErrors(value);
                }
            }
        }

        public void AddError(UserError error)
        {
            error.CheckArgumentNull(nameof(error));
            _errors.Add(error);
        }

        public void AddErrors(IEnumerable<UserError> errors)
        {
            _errors.CheckArgumentNull();
            foreach (var error in errors)
            {
                AddError(error);
            }
        }
    }
}
