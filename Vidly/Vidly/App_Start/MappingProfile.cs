using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Vidly.Models;
using Vidly.Dtos;

namespace Vidly.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            // Domain to Dto
            Mapper.CreateMap<Movie, MovieDto>();
            Mapper.CreateMap<Customer, CustomerDto>();
            Mapper.CreateMap<MembershipType, MembershipTypeDto>();

            // Dto to Domain
            /*
             Id is the key property for the Movie class, and a key property should not be changed. 
             That’s why we get this exception. To resolve this, you need to tell AutoMapper to ignore Id during mapping of a MovieDto to Movie. 
             */
            Mapper.CreateMap<CustomerDto, Customer>().
                ForMember(c => c.Id, opt => opt.Ignore()); ;
            Mapper.CreateMap<MovieDto, Movie>().
                ForMember(m => m.Id, opt =>  opt.Ignore());

        }
    }
}