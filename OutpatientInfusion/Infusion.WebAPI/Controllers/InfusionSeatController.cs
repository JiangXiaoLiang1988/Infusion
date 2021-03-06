﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infusion.Common.Entities;
using Infusion.DAL;
using Infusion.Domain.Repository;
using log4net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Infusion.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfusionSeatController : ControllerBase
    {
        // 日志
        private ILog log;

        private readonly IInfusionSeatRepository _infusionSeatRepository;
        /// <summary>
        /// 通过构造函数注入日志
        /// </summary>
        /// <param name="hostingEnv"></param>
        public InfusionSeatController(IHostingEnvironment hostingEnv, IInfusionSeatRepository seatRepository)
        {
            this.log = LogManager.GetLogger(Startup.repository.Name, typeof(InfusionSeatController));
            _infusionSeatRepository = seatRepository;
        }

        #region 获取所有输液室座位
        [HttpGet]
        public JsonResult GetAllInfusionSeat()
        {
            List<InfusionSeat> listSeat = new List<InfusionSeat>();
            try
            {
                listSeat = _infusionSeatRepository.GetAll();
            }
            catch (Exception ex)
            {
                log.Error("获取输液室全部座位失败:" + ex.Message + "\r\n跟踪:" + ex.Source);
            }
            return new JsonResult(listSeat);
        }
        #endregion

        #region 新增输液室座位

        [HttpPost]
        public bool PostInsusionSeat([FromBody]InfusionSeat infusionSeat)
        {
            bool tfSuccess = true;
            try
            {
                //using (var dbContext = new EFInfusionDbContext())
                //{
                //    // 设置状态为新增
                //    dbContext.Entry(infusionSeat).State = EntityState.Added;
                //    tfSuccess = dbContext.SaveChanges() > 0 ? true : false;
                //}
                tfSuccess = _infusionSeatRepository.Add(infusionSeat);
            }
            catch (Exception ex)
            {
                tfSuccess = false;
                log.Error("新增输液室全部座位失败:" + ex.Message + "\r\n跟踪:" + ex.Source);
            }
            return tfSuccess;
        }
        #endregion

        #region 修改输液室座位
        [HttpPut]
        public bool PutInfusionSeat([FromBody] InfusionSeat infusionSeat)
        {
            bool tfSuccess = true;
            try
            {
                //using (var dbContext = new EFInfusionDbContext())
                //{
                //    dbContext.Entry(infusionSeat).State = EntityState.Modified;
                //    tfSuccess = dbContext.SaveChanges() > 0 ? true : false;
                //}
                tfSuccess = _infusionSeatRepository.Modify(infusionSeat);
            }
            catch (Exception ex)
            {
                tfSuccess = false;
                log.Error("修改输液室全部座位失败:" + ex.Message + "\r\n跟踪:" + ex.Source);
            }
            return tfSuccess;
        }
        #endregion

        #region 删除输液室座位
        //[HttpDelete("{id}")]
        [HttpDelete]
        public bool Delete(int id)
        {
            bool tfSuccess = true;
            try
            {
                //using (var dbContext = new EFInfusionDbContext())
                //{
                //    InfusionSeat infusionSeat = new InfusionSeat()
                //    {
                //        InfusionId = id
                //    };
                //    // 设置状态是删除
                //    dbContext.Entry(infusionSeat).State = EntityState.Deleted;
                //    tfSuccess = dbContext.SaveChanges() > 0 ? true : false;
                //}
                tfSuccess = _infusionSeatRepository.Delete(id);
            }
            catch (Exception ex)
            {
                tfSuccess = false;
                log.Error("删除输液室全部座位失败:" + ex.Message + "\r\n跟踪:" + ex.Source);
            }
            return tfSuccess;
        }
        #endregion
    }
}