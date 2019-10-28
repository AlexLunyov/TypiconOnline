using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TypiconOnline.Domain.Identity;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.WebServices.Identity
{
    public class UserStore : IUserStore<User>, IUserPasswordStore<User>//, IUserEmailStore<User>
    {
        private readonly TypiconDBContext db;

        public UserStore(TypiconDBContext db)
        {
            this.db = db;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                db?.Dispose();
            }
        }

        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException(nameof(SetUserNameAsync));
        }

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException(nameof(GetNormalizedUserNameAsync));
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            return Task.FromResult((object)null);
        }

        public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            db.Add(user);

            await db.SaveChangesAsync(cancellationToken);

            return await Task.FromResult(IdentityResult.Success);
        }

        public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            db.Update(user);

            await db.SaveChangesAsync(cancellationToken);

            return await Task.FromResult(IdentityResult.Success);
        }

        public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            db.Remove(user);

            int i = await db.SaveChangesAsync(cancellationToken);

            return await Task.FromResult(i == 1 ? IdentityResult.Success : IdentityResult.Failed());
        }

        public async Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            if (int.TryParse(userId, out int id))
            {
                return await db.Set<User>().FindAsync(id);
            }
            else
            {
                return await Task.FromResult((User)null);
            }
        }

        public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return await db.Set<User>()
                .FirstOrDefaultAsync(p => p.UserName.Equals(normalizedUserName, StringComparison.OrdinalIgnoreCase), cancellationToken);
        }

        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;

            return Task.FromResult((object)null);
        }

        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(!string.IsNullOrWhiteSpace(user.PasswordHash));
        }

        //private void ThrowIfDisposed()
        //{
        //    if (_disposed)
        //    {
        //        throw new ObjectDisposedException(GetType().Name);
        //    }
        //}

        #region IUserEmailStore

        /// <summary>
        /// Used to attach child entities to the User aggregate, i.e. Roles, Logins, and Claims
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        //protected virtual async Task<User> GetUserAggregateAsync(Expression<Func<User, bool>> filter)
        //{
        //    int id;
        //    User user;
        //    if (FindByIdFilterParser.TryMatchAndGetId(filter, out id))
        //    {
        //        user = await _userStore.GetByIdAsync(id).WithCurrentCulture();
        //    }
        //    else
        //    {
        //        user = await Users.FirstOrDefaultAsync(filter).WithCurrentCulture();
        //    }
        //    if (user != null)
        //    {
        //        await EnsureClaimsLoaded(user).WithCurrentCulture();
        //        await EnsureLoginsLoaded(user).WithCurrentCulture();
        //        await EnsureRolesLoaded(user).WithCurrentCulture();
        //    }
        //    return user;
        //}

        /// <summary>
        ///     Find a user by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        //public virtual Task<User> FindByEmailAsync(string email)
        //{
        //    ThrowIfDisposed();
        //    return GetUserAggregateAsync(u => u.Email.ToUpper() == email.ToUpper());
        //}

        #endregion
    }
}
