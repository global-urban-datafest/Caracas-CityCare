using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using CityCare.Areas.Service.Models;
using CityCare.Models;
using Common.Models;
using FORCOUtils;
using FORCOUtils.DALUtils;

namespace CityCare.Areas.Service.Controllers
{
    public class CCUserController : ApiController
    {
        IGenericRepository<CCUser> fUserRepository = new EntityFrameworkDataRepository<CCUser>(new CityCareEntities());
        IGenericRepository<UserIdType> fUserIdTypeRepository = new EntityFrameworkDataRepository<UserIdType>(new CityCareEntities());
        


        [ResponseType(typeof(RequestResponseModel))]
         public HttpResponseMessage RegisterUser(RegisterUserModel aRegisterUserModel )
        {
            try
            {
                //todo add validations
                var _NewUser = new CCUser
                {
                    Email = aRegisterUserModel.Email,
                    Name = aRegisterUserModel.Name,
                    Password = aRegisterUserModel.Password,
                    UserLevel = 0,
                    PersonalId = "ID",
                    PersonalIdTypeId = fUserIdTypeRepository.GetSingleLazyLoading().Id
                };


                fUserRepository.Add(_NewUser);
                return Request.CreateResponse(HttpStatusCode.Created);
            }
            catch (Exception)
            {
                var _ResponseStatus = new RequestResponseModel
                {
                    Status = ResponseStatus.Error,
                    ErroMessage = "Error creating the user, please try again."
                };
                return Request.CreateResponse(HttpStatusCode.InternalServerError, _ResponseStatus);
            }
        }

        [ResponseType(typeof (RequestResponseModel))]
        public HttpResponseMessage ValidateUser(UserAuthDataModel aUserAuthDataModel)
        {
            try
            {
                if (fUserRepository.GetSingleLazyLoading(aUser => aUser.Email == aUserAuthDataModel.Email && aUser.Email == aUserAuthDataModel.Password) != null)
                {
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
            catch (Exception)
            {
                var _ResponseStatus = new RequestResponseModel
                {
                    Status = ResponseStatus.Error,
                    ErroMessage = "Error creating the user, please try again."
                };
                return Request.CreateResponse(HttpStatusCode.InternalServerError, _ResponseStatus);
            }
        }

        public HttpResponseMessage AddReports()
        {

        }
    }
}
