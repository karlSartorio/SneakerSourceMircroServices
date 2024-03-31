using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SneakerSource.Services.CouponAPI.Controllers.Models;
using SneakerSource.Services.CouponAPI.Controllers.Models.Transfer;
using SneakerSource.Services.CouponAPI.Data;

namespace SneakerSource.Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponAPIController : ControllerBase
    {
        // can make a service layer for this.
        private readonly AppDbContext _db;
        private ResponseDTO _response;
        private IMapper _mapper;
        public CouponAPIController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _response = new ResponseDTO();
            _mapper = mapper;
        }
        [HttpGet]
        public ResponseDTO Get()
        {
            try
            {
                IEnumerable<Coupon> couponLists = _db.Coupon.ToList();
                var result = _mapper.Map<IEnumerable<CouponDTO>>(couponLists);
                _response.Sucessful(result);
            }
            catch (Exception exc)
            {
                _response.Unsuccessful(exc.Message);

            }
            return _response;
        }
        [HttpGet]
        [Route("{id:int}")]
        public ResponseDTO Get(int id)
        {
            try
            {
                Coupon coupon = _db.Coupon.FirstOrDefault(n => n.Id == id);
                var result = _mapper.Map<CouponDTO>(coupon);
                if (coupon == null)
                {
                    _response.Unsuccessful($"Unable to find find coupon for Id: {id}");
                }
                _response.Sucessful(result);
            }
            catch (Exception exc)
            {
                _response.Unsuccessful(exc.Message);
            }
            return _response;
        }
        [HttpGet]
        [Route("GetByCode/{code}")]
        public ResponseDTO GetByCode(string code)
        {
            try
            {
                Coupon coupon = _db.Coupon.FirstOrDefault(n => n.Code == code);
                var result = _mapper.Map<CouponDTO>(coupon);
                if (coupon == null)
                {
                    _response.Unsuccessful($"Unable to find find coupon for Code: {code}");
                }
                _response.Sucessful(result);
            }
            catch (Exception exc)
            {
                _response.Unsuccessful(exc.Message);
            }
            return _response;
        }
        [HttpPut]
        public ResponseDTO Put([FromBody] CouponDTO coupon)
        {
            try
            {
                if (coupon == null)
                {
                    _response.Unsuccessful($"Please provide an coupon");
                }
                Coupon toUpdate = _db.Coupon.FirstOrDefault(n => n.Id == coupon.Id);
                if (toUpdate == null)
                {
                    _response.Unsuccessful($"Cannot find coupon being edited");
                }
                toUpdate.PullProperties(coupon);
                _db.SaveChanges();
                _response.Sucessful();
            }
            catch (Exception exc)
            {
                _response.Unsuccessful(exc.Message);
            }
            return _response;
        }
        [HttpPost]
        public ResponseDTO Post([FromBody] CouponDTO coupon)
        {
            try
            {
                if (coupon == null)
                {
                    _response.Unsuccessful($"Please provide an coupon");
                }
                Coupon isExisting = _db.Coupon.FirstOrDefault(n => n.Id == coupon.Id);
                if (isExisting != null)
                {
                    _response.Unsuccessful($"Coupon has already been created");
                }
                var newCoupon = _mapper.Map<Coupon>(coupon);
                _db.Coupon.Add(newCoupon);
                _db.SaveChanges();
                _response.Sucessful();
            }
            catch (Exception exc)
            {
                _response.Unsuccessful(exc.Message);
            }
            return _response;
        }
        [HttpDelete]
        public ResponseDTO Delete(int id)
        {
            try
            {
                Coupon coupon = _db.Coupon.FirstOrDefault(n => n.Id == id);
                if (coupon == null)
                {
                    _response.Unsuccessful($"Unable to find find coupon for Id: {id}");
                }
                _db.Coupon.Remove(coupon);
                _db.SaveChanges();
                _response.Sucessful();
            }
            catch (Exception exc)
            {
                _response.Unsuccessful(exc.Message);
            }
            return _response;
        }
    }
}
