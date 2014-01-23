
namespace Nancy.Bandit.IntegrationTest.Modules.ModuleTest
{
    using Nancy.Bandit.IntegrationTest.ViewModels.WeightedRewardTest;

    public class WeightedRewardTestModule : NancyModule
    {
        public WeightedRewardTestModule(IBandit bandit)
        {
            Get["/"] = parameters =>
            {
                var model = new WeightedRewardTestViewModel(){
                    Money = bandit.Choose("OptimalBegging")
                };

                return View["LandingPage.spark", model];
            };

            Post["/"] = parameters =>
            {
                string money = this.Request.Form["money"].Value;
                decimal reward;
                decimal.TryParse(money, out reward);

                bandit.Convert("OptimalBegging", reward);

                return View["ThankYou.spark", money];
            };
        }
    }
}