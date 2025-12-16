using Emergency_Services_Locator.Backend.Tools;
using Microsoft.EntityFrameworkCore;

namespace Emergency_Services_Locator.Backend.Functions
{
    public class facility_function
    {
        private readonly ApplicationDbContext _context;

        public facility_function(ApplicationDbContext context)
        {
            _context = context;
        }

        //public async Task<List<FacilityViewModel>> getFacilities()
        //{
        //    var facilities = await _context.Facilities
        //        .Where(c => !c.is_deleted)
        //        .Select(c => new FacilityViewModel(c))
        //        .ToListAsync();
        //    return facilities;
        //}
        public async Task<List<FacilityViewModel>> getFacilities()
        {
            var facilities = await _context.Facilities
                 .Join(_context.Maps,
                    facilities => facilities.map_id,
                    map => map.id,
                    (facilities, map) => new { facilities, map })
                 .Where(c => !c.facilities.is_deleted)
                 .Select(c => new FacilityViewModel
                 {
                     id = c.facilities.id,
                     facility_name = c.facilities.facility_name,
                     address = c.facilities.address,
                     contact = c.facilities.contact,
                     map_name = c.map.location_name
                 })
                 .ToListAsync();
            return facilities;
        }

        public async Task<int> createFacility()
        {

        }
    }
}
