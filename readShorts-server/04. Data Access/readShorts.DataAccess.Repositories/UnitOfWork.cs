using Framework.DataAccess.Interfaces;
using readShorts.DataAccess.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;

namespace readShorts.DataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private DataContext dbContext;
        private readonly IDatabaseFactory dbFactory;

        public UnitOfWork(IDatabaseFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }

        private DataContext DbContext
        {
            get
            {
                return dbContext ?? dbFactory.Get();
            }
        }

        public void SaveChanges()
        {
            try
            {
                var valid = DbContext.GetValidationErrors();
                if (valid == null || (valid != null && ((IList)valid).Count == 0))
                    DbContext.SaveChanges();
                else
                {
                    foreach (DbEntityEntry entry in DbContext.ChangeTracker.Entries())
                    {
                        switch (entry.State)
                        {
                            case EntityState.Modified:
                                entry.State = EntityState.Unchanged;
                                break;
                            case EntityState.Added:
                                entry.State = EntityState.Detached;
                                break;
                            case EntityState.Deleted:
                                entry.Reload();
                                break;
                            default: break;
                        }
                    }
                    throw new DbEntityValidationException("Failed on validation", valid);
                }
            }
            catch (OptimisticConcurrencyException)
            {
                DbContext.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                string errorMessages = string.Empty;
                foreach (DbEntityValidationResult validationResult in ex.EntityValidationErrors)
                {
                    foreach (DbValidationError validationError in validationResult.ValidationErrors)
                    {
                        errorMessages += validationError.ErrorMessage;
                    }
                }

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
            catch (EntityCommandExecutionException ex)
            {
                string errorMessages = string.Empty;
                foreach (KeyValuePair<string, string> item in ex.Data)
                {
                    errorMessages += string.Format("{0} - {1}", item.Key, item.Value);
                    var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", errorMessages);
                    throw new EntityCommandExecutionException(exceptionMessage);
                }
            }
            catch (Exception ex)
            {
                throw new DbUpdateException("Save data fail with unknown exception " + ex.Message);
            }
        }
    }
}
