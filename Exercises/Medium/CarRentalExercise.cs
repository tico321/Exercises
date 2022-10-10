using System;
using System.Collections.Generic;
using Xunit;

namespace Exercises.Medium
{
    public sealed class CarRentalExercise
    {
        /// <summary>
        /// Extend the code a write a test for the following scenario, our car rentals 3 pandas, 2 teslas, 1 golf.
        /// Customer Bob call our service asking for a golf, for 5 days starting on the 1st of April.
        /// Unfortunately the golf is already rented up to the 3rd of April.
        /// Our service tells Bob that we have two available teslas, and they can provide an individual discount of
        /// 10 euros per day.
        /// </summary>
        [Fact]
        public void Renting_with_dates()
        {
            // Arrange
            var panda = new Car()
            {
                Cost = 30,
                Name = "panda",
                IsRented = true,
                DaysRented = 4,
                RentedDate = new DateTime(2022, 03, 28)
            };
            var panda1 = new Car()
            {
                Cost = 30,
                Name = "panda",
                IsRented = true,
                DaysRented = 4,
                RentedDate = new DateTime(2022, 03, 28)
            };
            var panda2 = new Car()
            {
                Cost = 30,
                Name = "panda",
                IsRented = true,
                DaysRented = 4,
                RentedDate = new DateTime(2022, 03, 28)
            };
            var tesla = new Car()
            {
                Cost = 30,
                Name = "tesla",
            };
            var tesla1 = new Car()
            {
                Cost = 30,
                Name = "tesla",
            };
            var golf = new Car()
            {
                Cost = 30,
                Name = "golf",
            };
            var carRental = new CarRental(
                new List<Car>{ panda, panda1, panda2, tesla, tesla1, golf  });

            // Act
            var rentedOnDateResponse = carRental.RentOnDate(panda, new DateTime(2022, 04, 1), 5);

            // Assert
            Assert.False(rentedOnDateResponse.RentedSuccessfully);
        }

        [Fact]
        public void Renting_with_dates1()
        {
            // Arrange
            var panda = new Car()
            {
                Cost = 30,
                Name = "panda",
                IsRented = true,
                DaysRented = 4,
                RentedDate = new DateTime(2022, 03, 28)
            };
            var panda1 = new Car()
            {
                Cost = 30,
                Name = "panda",
                IsRented = true,
                DaysRented = 4,
                RentedDate = new DateTime(2022, 03, 28)
            };
            var panda2 = new Car()
            {
                Cost = 30,
                Name = "panda",
                IsRented = true,
                DaysRented = 4,
                RentedDate = new DateTime(2022, 03, 28)
            };
            var tesla = new Car()
            {
                Cost = 30,
                Name = "tesla",
            };
            var tesla1 = new Car()
            {
                Cost = 30,
                Name = "tesla",
            };
            var golf = new Car()
            {
                Cost = 30,
                Name = "golf",
            };
            var carRental = new CarRental(
                new List<Car>{ panda, panda1, panda2, tesla, tesla1, golf  });

            // Act
            var rentedOnDateResponse = carRental.RentOnDate(panda, new DateTime(2022, 04, 1), 5);

            // Assert
            Assert.False(rentedOnDateResponse.RentedSuccessfully);
            Assert.Equal(3, rentedOnDateResponse.AvailableAlternatives.Count);
        }

        [Fact]
        public void Renting_with_dates_with_end_in_the_middle_of_other_renting_range()
        {
            // Arrange
            var panda = new Car()
            {
                Cost = 30,
                Name = "panda",
                IsRented = true,
                DaysRented = 4,
                RentedDate = new DateTime(2022, 03, 28)
            };
            var carRental = new CarRental(new List<Car>{ panda  });
            var rentingStartDate = new DateTime(2022, 03, 25); // before the panda rented day
            var rentingDays = 5; // makes the end date be in the range of the already rented day


            // Act
            var rentedOnDateResponse = carRental.RentOnDate(panda, rentingStartDate, rentingDays);

            // Assert
            Assert.False(rentedOnDateResponse.RentedSuccessfully);
            Assert.Empty(rentedOnDateResponse.AvailableAlternatives); // no other available cars
        }

        [Fact]
        public void Get_cost()
        {
            // Arrange
            var panda = new Car()
            {
                Cost = 30,
                Name = "panda",
                IsRented = true,
                DaysRented = 5,
                RentedDate = new DateTime()
            };
            var carRental = new CarRental(new List<Car>{ panda });

            // Act
            var cost = carRental.GetCost();

            // Assert
            Assert.Equal(150, cost);
        }
    }

    public class Car
    {
        public int Cost { get; set; }
        public string Name { get; set; }
        public bool IsRented { get; set; }
        public DateTime RentedDate { get; set; }
        public int DaysRented { get; set; }
    }

    public class RentedOnDateResponse
    {
        public bool RentedSuccessfully { get; set; }
        public List<Car> AvailableAlternatives { get; set; }
    }

    public class CarRental
    {
        public CarRental(List<Car> cars)
        {
            Cars = cars;
        }

        public List<Car> Cars { get; set; }


        public RentedOnDateResponse RentOnDate(Car car, DateTime date, int days)
        {
            foreach (var availableCar in Cars)
            {
                if (car.Name == availableCar.Name && IsAvailableOnDate(availableCar, date, days))
                {
                    availableCar.IsRented = true;
                    availableCar.DaysRented = days;
                    availableCar.RentedDate = date;

                    return new RentedOnDateResponse
                    {
                        RentedSuccessfully = true,
                    };
                }
            }

            var availableCars = new List<Car>();
            foreach (var availableCar in Cars)
            {
                if (IsAvailableOnDate(availableCar, date, days))
                {
                    availableCars.Add(availableCar);
                }
            }

            return new RentedOnDateResponse
            {
                RentedSuccessfully = false,
                AvailableAlternatives = availableCars,
            };
        }
        private bool IsAvailableOnDate(Car car, DateTime start, int days)
        {
            var alreadyRentedStart = car.RentedDate;
            var alreadyRentedEnd = car.RentedDate.AddDays(car.DaysRented);
            if (start > alreadyRentedEnd)
            {
                return true; // is available
            }

            if (start >= alreadyRentedStart)
            {
                return false; // is rented
            }

            var end = start.AddDays(days);
            if (start < alreadyRentedStart && end < alreadyRentedStart)
            {
                return true; // is available
            }

            return false; // is rented
        }

        public int GetCost()
        {
            var totalCost = 0;
            foreach (var car in Cars)
            {
                if (car.IsRented)
                {
                    totalCost += car.Cost * car.DaysRented;
                }
            }

            return totalCost;
        }
    }
}

