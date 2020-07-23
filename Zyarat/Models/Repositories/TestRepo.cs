using System.Collections.Generic;
using Zyarat.Data;

namespace Zyarat.Models.Repositories
{
    public class TestRepo:ContextRepo
    {
        public TestRepo(ApplicationContext context) : base(context)
        {
        }

        public void AddData()
        {
            var govList= new List<Government>
            {
                new Government {Gov = "Sohag",Cities =new List<City>
                {
                    new City
                    {
                        CityName = "Tahta",
                        Doctors = new List<Doctor>
                        {
                            new Doctor()
                            {
                                FName = "Motaz",
                                LName = "Saed"
                            }
                        }
                    }
                } },
                new Government {Gov = "Assuit",Cities = new List<City>
                {
                    new City
                    {
                        CityName = "Neda",
                        Doctors = new List<Doctor>
                        {
                            new Doctor()
                            {
                                FName = "Motaz2",
                                LName = "Saed2"
                            }
                        }
                    },
                    new City
                    {
                        CityName = "Neda",
                        Doctors = new List<Doctor>
                        {
                            new Doctor
                            {
                                FName = "Motaz3",
                                LName = "Saed3",
                            }
                        }
                    }
                    
                }},
                new Government {Gov = "Quena",Cities = new List<City>
                {
                    
                }},
                new Government {Gov = "Cairo",Cities = new List<City>
                {
                    
                }},
            };
            
            











































        }
    }
}