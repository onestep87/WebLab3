using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.db
{
    public class DropdownManagment : Lab3Client
    {
        public async Task DeleteDropdown(int number)
        {
            await using (context)
            {
                var allDropdown = context.Dropdowns.ToListAsync()
                    .Result;

                for (int i = 0; i < allDropdown.Count; i++)
                {
                    if (allDropdown[i].Number == number)
                    {
                        context.Remove(allDropdown[i]);
                    }
                }
                context.SaveChanges();
            }
        }
        public async Task<IEnumerable> GetNumbers()
        {
            await using (context)
            {
                var allLinks = context.Dropdowns.ToListAsync().Result;

                List<int> allNumbers = new List<int>();

                for (int i = 0; i < allLinks.Count; i++)
                {
                    allNumbers.Add(allLinks[i].Number);
                }

                List<int> numbers = new List<int>();

                for (int i = 0; i < allNumbers.Count; i++)
                {
                    if (!numbers.Contains(allNumbers[i]))
                    {
                        numbers.Add(allNumbers[i]);
                    }
                }

                return numbers;
            }
        }

        public async Task<IEnumerable> GetDropdown(int number)
        {
            await using (context)
            {
                var allLinks = context.Dropdowns.ToListAsync().Result;

                string[] response;
                if (number == -1)
                {
                    List<int> allNumbers = new List<int>();

                    for (int i = 0; i < allLinks.Count; i++)
                    {
                        allNumbers.Add(allLinks[i].Number);
                    }

                    var necessaryLinks = allLinks.Where(dropdown => dropdown.Number == allNumbers.Max());

                    var sortedLinks = from u in necessaryLinks orderby u.Queque ascending select u;
                    response = new string[sortedLinks.Count()];
                    for (int i = 0; i < sortedLinks.Count(); i++)
                    {
                        response[i] = sortedLinks.ToList()[i].Data;
                    }
                }
                else
                {
                    var necessaryLinks = allLinks.Where(dropdown => dropdown.Number == number);

                    var sortedLinks = from u in necessaryLinks orderby u.Queque ascending select u;

                    response = new string[sortedLinks.Count()];
                    for (int i = 0; i < sortedLinks.Count(); i++)
                    {
                        response[i] = sortedLinks.ToList()[i].Data;
                    }
                }

                return response;
            }
        }
        public async Task CreateDropdown(List<string> data)
        {
            await using (context)
            {
                var allDropdown = context.Dropdowns.ToListAsync()
                    .Result;

                if(allDropdown.Count == 0)
                {
                    for (int i = 0; i < data.Count; i++)
                    {
                        Dropdown item = new Dropdown
                        {
                            Number = 1,
                            Queque = i + 1,
                            Data = data[i]
                        };

                        context.Add(item);
                    }
                }
                else
                {
                    List<int> allNumbers = new List<int>();

                    for (int i = 0; i < allDropdown.Count; i++)
                    {
                        allNumbers.Add(allDropdown[i].Number);
                    }

                    for (int i = 0; i < data.Count; i++)
                    {
                        Dropdown item = new Dropdown
                        {
                            Number = allNumbers.Max() + 1,
                            Queque = i + 1,
                            Data = data[i]
                        };

                        context.Add(item);
                    }
                }
                
                context.SaveChanges();
            }
        }
    }
}
