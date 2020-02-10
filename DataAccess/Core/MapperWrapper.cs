using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using AutoMapper;
using DataAccess.MapperProfiles;

namespace DataAccess.Core
{
    class MapperWrapper
    {
        private static IMapper mapper;

        public static IMapper GetMapper()
        {
            if (mapper != null)
            {
                return mapper;
            }

            MapperConfiguration configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(AssetBalanceInfoProfile).Assembly);
            });

            mapper = configuration.CreateMapper();
            return mapper;
        }
    }
}
