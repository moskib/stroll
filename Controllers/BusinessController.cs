using System;
using Microsoft.AspNetCore.Mvc;
using Stroll.Data;
using Stroll.Interfaces;
using Stroll.Services;

namespace Stroll.Controllers
{
    [Route("api/[controller]")]
    public class BusinessController: ControllerBase
    {
        private readonly IUnitOfWork _repo;

        public BusinessController(ApplicationDbContext context)
        {
            _repo = new UnitOfWork(context);
        }
    }
}
