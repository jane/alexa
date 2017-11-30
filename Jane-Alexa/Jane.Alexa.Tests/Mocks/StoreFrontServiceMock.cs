using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Jane.Alexa.Models;
using Jane.Alexa.Services;

namespace Jane.Alexa.Tests.Mocks
{
    public class StoreFrontServiceMock : IStoreFrontService
    {
        public Task<List<StorefrontItem>> GetStorefront(int take = 5, bool filterSoldOut = true, bool filterEndingSoon = true, bool filterIsNew = true)
        {
            var storeFrontItems = new List<StorefrontItem>();
            var random = new Random();

            for (int i = 0; i < take; i++)
            {
                storeFrontItems.Add(new StorefrontItem()
                {
                    DealId = random.Next(1, 100000),
                    IsDeliveredElectronically = true,
                    IsEndingSoon = filterEndingSoon,
                    IsNew = filterIsNew,
                    IsSoldOut = filterSoldOut,
                    LikeCount = random.Next(0, 100),
                    Price = random.Next(5, 30),
                    Quantity = random.Next(1, 100),
                    Title = "Fake Deal " + random.Next(1, 10000),
                    Url = "www.fake.com"
                });
            }

            return Task.Run(() => storeFrontItems);
        }
    }
}
