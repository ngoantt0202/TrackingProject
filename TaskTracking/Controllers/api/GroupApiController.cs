using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaskTracking.DbContext;
using TaskTracking.DTOs;
using TaskTracking.Models;

namespace TaskTracking.Controllers.api
{
    [Authorize]
    [RoutePrefix("api/Group")]
    public class GroupApiController : ApiController
    {
        TrackingDbContext db = new TrackingDbContext();

        private static readonly Expression<Func<t_group, GroupViewModels>> AsModelViews = x => new GroupViewModels
        {
           GroupId = x.GroupId,
           GroupName = x.GroupName,
           GroupType = x.GroupType,
           Members = x.Members,
           Description = x.Description
        };

        private static readonly Expression<Func<t_memeber, MemberViewModels>> AsMemberViews = x => new MemberViewModels
        {
            MemberId = x.Member,
            Email = x.Email,
            Name = x.Name,
            Status = x.Status
        };

        [Route("CreateGroup")]
        [HttpPost]
        public IHttpActionResult CreateGroup(GroupViewModels create)
        {
            using(var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    // add group
                    var group = new t_group()
                    {
                        GroupName = create.GroupName,
                        GroupType = create.GroupType,
                        Description = create.Description,
                        Members = create.Members
                    };
                    db.t_group.Add(group);
                    db.SaveChanges();
                    // add members
                    int groupId = group.GroupId;
                    foreach(var item in create.TagList)
                    {
                        var member = new t_memeber
                        {
                            Name = item,
                            Email = item,
                            GroupId = groupId,
                            Status = "1"
                        };
                        db.t_memeber.Add(member);
                    }
                    db.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return NotFound();
                }
            }

            return Ok();
        }

        [Route("GetAll")]
        [HttpGet]
        public IQueryable<GroupViewModels> GetAllGroup()
        {
            return db.t_group.Select(AsModelViews);
        }

        [Route("GetSummaryGroupForDay")]
        [HttpGet]
        public IHttpActionResult GetSummaryGroupForDay()
        {
           var summaryGroups = (from g in db.t_group join m in db.t_memeber
                                on g.GroupId equals m.GroupId
                               // where g.GroupId == 1
                                group new {g,m} by new {g.GroupId} into grp
                                select new SummaryGroupDto
                                {
                                    GroupId = grp.Key.GroupId,
                                    GroupName = grp.Select(k => k.g.GroupName).FirstOrDefault(),
                                    RemainMembers = grp.Sum(k => (k.m.Status == "1" ? 1 : 0)),
                                    LeaveMembers = grp.Sum(k => (k.m.Status != "1" ? 1 : 0)),
                                    TotalMembers = grp.Sum(k => (k.m.Status == "1" ? 1 : 0)) + grp.Sum(k => (k.m.Status != "1" ? 1 : 0))
                                }).ToList();
            if (summaryGroups == null || summaryGroups.Count == 0)
            {
                return NotFound();
            }

            return Ok(summaryGroups);
        }

        [Route("{groupId:int}/details")]
        public IHttpActionResult GetGroupDetail(int groupId)
        {
            var group = db.t_group.Where(n => n.GroupId == groupId)
                .Select(AsModelViews).FirstOrDefault();
            if (group == null)
            {
                return NotFound();
            }
            var members = db.t_memeber.Where(n => n.GroupId == groupId)
                .Select(AsMemberViews).ToList();
            if(members.Count == 0)
            {
                return NotFound();
            }
            group.MemberLst.AddRange(members);
            return Ok(group);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
