using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PROG3050_CVGSClub.Models
{
    public partial class CvgsClubContext
    {
        public Boolean EFValidation()
        {
            var serviceProvider = this.GetService<IServiceProvider>();
            var items = new Dictionary<object, object>();
            string errors = "";

            // go through each Add/Update entry in the EF queue and validate its object
            foreach (var entry in this.ChangeTracker.Entries()
                .Where(a => a.State == EntityState.Added || a.State == EntityState.Modified))
            {
                var entity = entry.Entity; // extract the object from its action                
                var context = new ValidationContext(entity, serviceProvider, items);
                var results = new List<ValidationResult>();

                if (Validator.TryValidateObject(entity, context, results, true) == false)
                {
                    foreach (var result in results)  // accumulate all error messages
                    {
                        if (result != ValidationResult.Success) errors = errors + $"::: {result.ErrorMessage}";
                    }
                }
            }
            // if any validation errors were found, throw them as an exception
            if (errors != "") throw new ValidationException(errors);
            else return true;
        }
        }
    }
