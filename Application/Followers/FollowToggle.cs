using Application.Core;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Followers
{
    public class FollowToggle
    {
        public class Command : IRequest<Result<Unit>>
        {
            public string TargetUsername { get; set; }
        }

        public class Hnadler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _contexet;
            private readonly IUserAccessor _userAccessor;
            public Hnadler(DataContext contexet, IUserAccessor userAccessor)
            {
                _userAccessor = userAccessor;
                _contexet = contexet;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var observer = await _contexet.Users.FirstOrDefaultAsync(x=> 
                    x.UserName == _userAccessor.GetUsername());

                var target = await _contexet.Users.FirstOrDefaultAsync(x => 
                        x.UserName == request.TargetUsername);

                if (target == null) return null;

                var following = await _contexet.UserFollowings.FindAsync(observer.Id, target.Id);

                if(following == null)
                {
                    following = new UserFollowing
                    {
                        Observer = observer,
                        Target = target
                    };

                    _contexet.UserFollowings.Add(following);
                }
                else
                {
                    _contexet.UserFollowings.Remove(following);
                }

                var success = await _contexet.SaveChangesAsync() > 0;

                if(success) return Result<Unit>.Success(Unit.Value);

                return Result<Unit>.Failure("Failed to update following");
            }
        }
    }
}