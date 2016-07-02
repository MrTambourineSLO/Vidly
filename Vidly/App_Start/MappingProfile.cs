using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Vidly.DTOs;
using Vidly.Models;

namespace Vidly.App_Start 
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to Dto
            Mapper.CreateMap<Customer, CustomerDto>();
            Mapper.CreateMap<Movie, MovieDto>();
                //For special case MembershipTypeDto, GenreDto
            Mapper.CreateMap<MembershipType, MembershipTypeDto>();
            Mapper.CreateMap<Genre, GenreDto>();
            //Dto to Domain
            //Id is a key property of domain classes and should not be changed
            //Here we tell automapper to ignore id assignments
            Mapper.CreateMap<CustomerDto, Customer>().
                ForMember(c => c.Id, opt => opt.Ignore());
            Mapper.CreateMap<MovieDto, Movie>().
                ForMember(c => c.Id, opt => opt.Ignore());



        }
    }
}