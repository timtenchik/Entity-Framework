using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;


namespace Entity_Framework_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            //LIKE
            Console.Write("\n--LIKE--\n");
            using (ApplicationContext db = new ApplicationContext())
            {
                var doctors = db.Doctors.Where(p => EF.Functions.Like(p.LastName, "%чук"));
                foreach (Doctor doctor in doctors)
                    Console.WriteLine($"{doctor.LastName} {doctor.FirstName} ");
            }

            //TOP 5
            Console.Write("\n--TOP 5--\n");
            using (ApplicationContext db = new ApplicationContext())
            {
                var patients = (from patient in db.Patients
                                select patient).Take(5);

                foreach (var patient in patients)
                    Console.WriteLine($"{patient.FirstName} {patient.LastName}");
            }

            //WHERE
            Console.Write("\n--WHERE--\n");
            using (ApplicationContext db = new ApplicationContext())
            {
                var patients = db.Patients.Where(p => p.FirstName == "Антон");
                foreach (Patient patient in patients)
                    Console.WriteLine($"{patient.FirstName} ({patient.LastName})");
            }

            //JOIN
            Console.Write("\n--JOIN--\n");
            using (ApplicationContext db = new ApplicationContext())
            {
                var patients = db.Patients.Join(db.Receptions,
                        u => u.Id, 
                        c => c.PatientId,
                        (u, c) => new 
                        {
                            Name = u.FirstName,
                            Date = c.Date
                        });
                foreach (var u in patients)
                    Console.WriteLine($"{u.Name} - {u.Date}");
            }

            //JOIN 3 TABLE
            Console.Write("\n--JOIN 3 TABLE--\n");
            using (ApplicationContext db = new ApplicationContext())
            {
                var users = from reseption in db.Receptions
                            join patient in db.Patients on reseption.PatientId equals patient.Id
                            join diagnos in db.Diagnosis on reseption.DiagnosisId equals diagnos.Id
                            select new
                            {
                                Name = patient.FirstName,
                                Disease = diagnos.Disease,
                                Date = reseption.Date
                            };
                foreach (var u in users)
                    Console.WriteLine($"{u.Name} ({u.Disease}) - {u.Date}");
            }

            //GROUP BY
            Console.Write("\n--GROUP BY--\n");
            using (ApplicationContext db = new ApplicationContext())
            {
                var groups = from u in db.Ascribes
                             group u by u.Drug into g
                             select new
                             {
                                 g.Key,
                                 Count = g.Count()
                             };
                foreach (var group in groups)
                {
                    if(group.Key != null)
                        Console.WriteLine($"{group.Count} - {group.Key}");
                }
            }
            
            //UNION
            Console.Write("\n--UNION--\n");
            using (ApplicationContext db = new ApplicationContext())
            {
                var Patients = db.Patients.Select(p => new { Name = p.FirstName, LastName = p.LastName })
                    .Union(db.Doctors.Select(c => new { Name = c.FirstName, LastName = c.LastName }));
                foreach (var patient in Patients)
                    Console.WriteLine($"{patient.Name} - {patient.LastName}");
            }

            //INTERSECT
            Console.Write("\n--INTERSECT--\n");
            using (ApplicationContext db = new ApplicationContext())
            {
                var users = db.Patients.Select(p => new { FirstName = p.FirstName})
                    .Intersect(db.Doctors.Select(c => new { FirstName = c.FirstName}));
                foreach (var user in users)
                    Console.WriteLine($"{user.FirstName}");
            }

            //EXCEPT
            Console.Write("\n--EXCEPT--\n");
            using (ApplicationContext db = new ApplicationContext())
            {
                var selector1 = db.Patients.Select(p => new { FirstName = p.FirstName, LastName = p.LastName }); 
                var selector2 = db.Doctors.Select(c => new { FirstName = c.FirstName, LastName = c.LastName }); 
                var users = selector1.Except(selector2); 

                foreach (var user in users)
                    Console.WriteLine($"{user.FirstName} - {user.LastName}");
            }

            //ORDER BY DESC
            Console.Write("\n--ORDER BY DESC--\n");
            using (ApplicationContext db = new ApplicationContext())
            {
                var patients = db.Patients.OrderByDescending(p => p.Birthday);
                foreach (var patient in patients)
                    Console.WriteLine($"{patient.Id}.{patient.Birthday} - {patient.FirstName} {patient.LastName}");
            }

            //MIN MAX AVG SUM
            Console.Write("\n--MIN MAX AVG SUM--\n");
            using (ApplicationContext db = new ApplicationContext())
            {
                decimal? minPrice = db.Ascribes.Min(u => u.Price);
                decimal? maxPrice = db.Ascribes.Max(u => u.Price);
                decimal? avgPrice = db.Ascribes.Average(u => u.Price);
                decimal? sumPrice = db.Ascribes.Sum(u => u.Price);

                Console.WriteLine($"MIN - {minPrice}\nMAX - {maxPrice}\nAVG - {avgPrice}\nSUM - {sumPrice}");
            }

            //PROCEDURE WITH PARAMETERS
            Console.Write("\n--PROCEDURE WITH PARAMETERS--\n");
            using (ApplicationContext db = new ApplicationContext())
            {
                var param1 = new Microsoft.Data.SqlClient.SqlParameter
                {
                    ParameterName = "@minPrice",
                    SqlDbType = System.Data.SqlDbType.Money,
                    Direction = System.Data.ParameterDirection.Output,
                    Size = 50
                };

                var param2 = new Microsoft.Data.SqlClient.SqlParameter
                {
                    ParameterName = "@maxPrice",
                    SqlDbType = System.Data.SqlDbType.Money,
                    Direction = System.Data.ParameterDirection.Output,
                    Size = 50
                };
                db.Database.ExecuteSqlRaw("GetPriceStats @minPrice OUT, @maxPrice OUT", param1, param2);
                Console.WriteLine($"{param1.Value} - {param2.Value}");
            }

            //FUNCTIONS
            Console.Write("\n--FUNCTIONS--\n");
            using (ApplicationContext db = new ApplicationContext())
            {
                var param1 = new SqlParameter("@min_price", 50);
                var param2 = new SqlParameter("@max_price", 200);
                var users = db.Ascribes.FromSqlRaw("SELECT * FROM GetAscribeByPrice (@min_price, @max_price)", param1, param2).ToList();
                foreach (var u in users)
                    Console.WriteLine($"{u.Drug} - {u.Price}");
            }

            //PROTECT (Вивести дні та кількість відвідувачів поліклініки в ці дні, В який день скільки було відвідувачів)
            Console.Write("\n--PROTECT--\n");
            using (ApplicationContext db = new ApplicationContext())
            {
                var groups = (from u in db.Receptions
                             group u by u.Date into g
                             select new
                             {
                                 g.Key,
                                 Count = g.Count()
                             }).OrderByDescending(p => p.Count);
                foreach (var group in groups)
                {
                    Console.WriteLine($"{group.Count} - {group.Key}");
                }
            }
            Console.Read();
        }
        void Add()
        {
            // Добавление
            using (ApplicationContext db = new ApplicationContext())
            {
                Patient patient1 = new Patient { Id = 4, FirstName = "Tom", LastName = "Lourence", Address = "м-н Перемоги 9Б" };
                Patient patient2 = new Patient { Id = 5, FirstName = "Alice", LastName = "Henckok", Address = "м-н Перемоги 16" };

                // Добавление
                db.Patients.Add(patient1);
                db.Patients.Add(patient2);
                db.SaveChanges();
            }

            // получение
            using (ApplicationContext db = new ApplicationContext())
            {
                var Patients = db.Patients.ToList();
                Console.WriteLine("Данные после добавления:");
                foreach (Patient pt in Patients)
                {
                    Console.WriteLine($"{pt.Id}.{pt.FirstName} - {pt.LastName}");
                }
            }


        }
        void Edit()
        {
            // Редактирование
            using (ApplicationContext db = new ApplicationContext())
            {
                // получаем первый объект
                Patient patient = db.Patients.FirstOrDefault();
                if (patient != null)
                {
                    patient.Id = 1;
                    patient.FirstName = "Oleg";
                    patient.LastName = "Gornov";
                    //обновляем объект
                    db.SaveChanges();
                }
                // выводим данные после обновления
                Console.WriteLine("\nДанные после редактирования:");
                var Patients = db.Patients.ToList();
                foreach (Patient pt in Patients)
                {
                    Console.WriteLine($"{pt.Id}.{pt.FirstName} - {pt.LastName}");
                }
            }
        }

        void Remove()
        {
            // Удаление
            using (ApplicationContext db = new ApplicationContext())
            {
                // получаем первый объект
                Patient patient = db.Patients.FirstOrDefault();
                if (patient != null)
                {
                    //удаляем объект
                    db.Patients.Remove(patient);
                    db.SaveChanges();
                }
                // выводим данные после обновления
                Console.WriteLine("\nДанные после удаления:");
                var Patients = db.Patients.ToList();
                foreach (Patient pt in Patients)
                {
                    Console.WriteLine($"{pt.Id}.{pt.FirstName} - {pt.LastName}");
                }
            }
        }
    }
}
