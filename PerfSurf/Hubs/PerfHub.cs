using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using PerfSurf.Counters;

namespace PerfSurf.Hubs {

    public class PerfHub : Hub {

        public PerfHub() {

            StartCounterCollection();

        }

        private void StartCounterCollection() {

            var task = Task.Factory.StartNew(async () => {

                //var perfService = new PerfCounterService();

                while (true) {

                    //var results = perfService.GetResults();

                    Clients.All.newCounters(new[] {
                          new { name = "Euler", value = new Random().Next(0, 100) }
                        , new { name = "Chavez", value = new Random().Next(0, 100) }
                        , new { name = "Hernandez", value = new Random().Next(0, 100) } }.AsEnumerable());

                    await Task.Delay(2000);

                }

            }, TaskCreationOptions.LongRunning);

        }

        public void Send(string message) {

            Clients.All.newMessage(Context.User.Identity.Name + " says " + message);

        }

    }

}