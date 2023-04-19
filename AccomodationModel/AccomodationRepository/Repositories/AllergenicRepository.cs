using AccomodationModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccomodationModel.AccomodationRepository.Repositories
{
    public class AllergenicRepository : RepositoryBase<Allergenic>
    {
        public AllergenicRepository(AccomodationContext context) : base(context)
        {
        }
    }
}
