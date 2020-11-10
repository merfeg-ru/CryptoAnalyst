using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HistoryExchange.Models
{
	public class AutoMapperProfiles : Profile
	{
		public AutoMapperProfiles()
		{
			CreateMap<HistoryItemShort, HistoryBinanceDTO>()
				.ForMember("Slice", slice => new SliceBinanceDTO { DateTime = DateTime.Now });
			
			//CreateMap<IUser, UserDTO>();

			//CreateMap<UserDTO, User>();

			//CreateMap<Organization, OrganizationDTO>();
			//CreateMap<OrganizationDTO, Organization>();
		}
	}
}
