using Microsoft.AspNetCore.Mvc;
using System.Net;
using InventoryService.Domain.User.Services;
using InventoryService.Infrastructure.Attributes;
using InventoryService.Constants.Permission;
using InventoryService.Domain.User.Dtos;
using InventoryService.Infrastructure.Helpers;

namespace InventoryService.Http.API.Version1.User.Controllers
{
    [Route("api/v1/users")]
    [ApiController]

    public class UserController(
        UserService userService
        ) : ControllerBase
    {
        private readonly UserService _userService = userService;

        [HttpGet()]
        [Permissions(PermissionConstant.USER_VIEW)]
        public async Task<ApiResponse> Index([FromQuery] UserQueryDto query)
        {
            var paginationResult = await _userService.Index(query);
            return new ApiResponsePagination<UserResultDto>(HttpStatusCode.OK, paginationResult);
        }

        [HttpGet("{id}")]
        [Permissions(PermissionConstant.USER_VIEW)]
        public async Task<ApiResponse> Show(Guid id)
        {
            Models.User data = await _userService.Detail(id);
            return new ApiResponseData<Models.User>(HttpStatusCode.OK, data);
        }

        [HttpPost()]
        [Permissions(PermissionConstant.USER_CREATE)]
        public async Task<ApiResponse> Store(UserCreateDto dataCreate)
        {
            await _userService.Create(dataCreate);
            return new ApiResponseData<Models.User>(HttpStatusCode.OK, null);
        }

        [HttpPut("{id}")]
        [Permissions(PermissionConstant.USER_UPDATE)]
        public async Task<ApiResponse> Update(Guid id, UserUpdateDto dataUpdate)
        {
            await _userService.Update(id, dataUpdate);
            return new ApiResponseData<Models.User>(HttpStatusCode.OK, null);
        }

        [HttpDelete("{id}")]
        [Permissions(PermissionConstant.USER_DELETE)]
        public async Task<ApiResponse> Delete(Guid id)
        {
            await _userService.Delete(id);
            return new ApiResponseData<Models.User>(HttpStatusCode.OK, null);
        }
    }
}
