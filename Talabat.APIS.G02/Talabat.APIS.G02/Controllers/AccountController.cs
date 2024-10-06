using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using Talabat.APIS.G02.DTOS;
using Talabat.APIS.G02.Errors;
using Talabat.APIS.G02.Extensions;
using Talabat.Core.Entities;
using Talabat.Core.Services;

namespace Talabat.APIS.G02.Controllers {

    public class AccountController : APIBaseController {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _token;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService token, IMapper mapper) {
            _userManager = userManager;
            _signInManager = signInManager;
            _token = token;
            _mapper = mapper;
        }

        [HttpPost("Register")]

        public async Task<ActionResult<UserDTO>> Register(RegisterDTO model) {

            if (EmailExists(model.Email).Result.Value) {
                return BadRequest(new ApiResponse(400, "Email Already in Use!"));
            }

            var User = new AppUser() {
                DisplayName = model.DisplayName,
                Email = model.Email,
                UserName = model.Email.Split('@')[0],
                PhoneNumber = model.PhoneNumber,
            };

            var Result = await _userManager.CreateAsync(User, model.Password);

            if (!Result.Succeeded) {
                return BadRequest(new ApiResponse((int)HttpStatusCode.BadRequest));
            }

            var ReturnUser = new UserDTO() {
                DisplayName = User.DisplayName,
                Email = User.Email,
                Token = await _token.CreateTokenAsync(User, _userManager)
            };

            return Ok(ReturnUser);
        }

        [HttpPost("Login")]

        public async Task<ActionResult<UserDTO>> Login(LoginDTO model) {
            var User = await _userManager.FindByEmailAsync(model.Email);

            if (User is null) {
                return Unauthorized(new ApiResponse((int)HttpStatusCode.Unauthorized));
            }

            var Result = await _signInManager.CheckPasswordSignInAsync(User, model.Password, false);

            if (!Result.Succeeded) {
                return Unauthorized(new ApiResponse((int)HttpStatusCode.Unauthorized));
            }

            return Ok(new UserDTO() {
                DisplayName = User.DisplayName,
                Email = User.Email,
                Token = await _token.CreateTokenAsync(User, _userManager)
            });
        }


        [HttpGet("GetCurrentUser")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<UserDTO>> GetCurrentUser() {

            var Email = User.FindFirstValue(ClaimTypes.Email);

            var appUser = await _userManager.FindByEmailAsync(Email);

            var ReturnUser = new UserDTO() {
                DisplayName = appUser.DisplayName,
                Email = appUser.Email,
                Token = await _token.CreateTokenAsync(appUser, _userManager)
            };

            return Ok(ReturnUser);
        }

        [HttpGet("GetUserAddress")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        public async Task<ActionResult<AddressDto>> GetUserAddress() {

            var user = await _userManager.FindCurrentUserAddress(User);
            var MappedAddress = _mapper.Map<Address, AddressDto>(user.Address);
            return Ok(MappedAddress);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("UpdateAddress")]

        public async Task<ActionResult<AddressDto>> UpdateAddress(AddressDto UpdatedAddress) {

            var user = await _userManager.FindCurrentUserAddress(User);

            if (user == null) {
                return Unauthorized(new ApiResponse((int)HttpStatusCode.Unauthorized));
            }

            var Address = _mapper.Map<AddressDto, Address>(UpdatedAddress);
            Address.Id = user.Address.Id;
            user.Address = Address;

            var Result = await _userManager.UpdateAsync(user);

            if (!Result.Succeeded) {
                return BadRequest(new ApiResponse((int)HttpStatusCode.BadRequest));
            }

            return Ok(UpdatedAddress);
        }
        [HttpGet("EmailExists")]
        public async Task<ActionResult<bool>> EmailExists(string Email) {

            var User = await _userManager.FindByEmailAsync(Email);

            return User is null? false : true;
        }
    }
}
