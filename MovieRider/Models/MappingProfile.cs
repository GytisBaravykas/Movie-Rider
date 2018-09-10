using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieRider.Dtos;

namespace MovieRider.Models
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            // Domain to Dto
            CreateMap<Customer, CustomerDto>();
            CreateMap<Movie, MovieDto>();
            CreateMap<MembershipType, MembershipTypeDto>();
            CreateMap<Genre, GenreDto>();

            // Dto to Domain
            CreateMap<CustomerDto, Customer>().ForMember(c => c.Id, opt => opt.Ignore());
            CreateMap<MovieDto, Movie>().ForMember(c => c.Id, opt => opt.Ignore());



        }
    }
}
