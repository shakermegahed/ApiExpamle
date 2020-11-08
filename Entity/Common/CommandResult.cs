using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class CommandResult<T>
    {
        public bool IsSucceeded { get; set; } = true;
        public List<String> Errors { get; set; } = new List<string>();
        public T Data { get; set; }

        public CommandResult()
        {
        }

        public CommandResult(T data)
        {
            this.Data = data;
        }

        public CommandResult(String error, string errorCode = "")
        {
            this.Errors.Add(error);
            this.IsSucceeded = false;
        }

        public CommandResult(List<String> errors)
        {
            this.Errors.AddRange(errors);
            this.IsSucceeded = false;
        }

        public CommandResult(IEnumerable<IdentityError> errors)
        {
            foreach (IdentityError error in errors)
            {
                this.Errors.Add($"{error.Code}: {error.Description}");
            }
            this.IsSucceeded = false;
        }
    }
}
