Nancy.Bandit
============

An unofficial multi armed bandit optimization framework for NancyFx.

Nancy.Bandit is a multi armed bandit optimization framework for [NancyFx](http://nancyfx.org/)  
Nancy.Bandit is heavily influenced by the [Ruby Bandit Gem](https://github.com/bmuller/bandit)  
Nancy.Bandit is maintained by [Cogslave](http://cogslave.com) and is not an official Nancy library


# Features
1. Pluggable architecture. (Persistence, selection algorithm, session handling)


# Installation
```csharp
PM> Install-Package Nancy.Bandit
```

# Configure
```c#
protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
{
    base.ApplicationStartup(container, pipelines);

    CookieBasedSessions.Enable(pipelines);
            
    pipelines.BeforeRequest += (ctx) =>
    {
        ((Bandido)container.Resolve<IBandit>()).Session = new NancySessionAdapter(ctx.Request.Session);
        return null;
    };
}

protected override void ConfigureApplicationContainer(TinyIoCContainer container)
{
    base.ConfigureApplicationContainer(container);

    IBandit bandit = new Bandido(new MemoryRepository());
    container.Register<IBandit>(bandit);
}
```

# Usage
```c#
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
```

# Tests
Unit tests are written using [xUnit](http://xunit.codeplex.com/)  
Code coverage is done using [OpenCover](https://github.com/OpenCover/opencover)  
Integration testing is done manually with a test Nancy site.


# Contributing
1. Check for [open issues](https://github.com/cogslave/Nancy.Bandit/issues) or open a fresh issue to start a discussion around a feature idea or a bug.
2. Fork the [Nancy.Bandit repository](https://github.com/cogslave/Nancy.Bandit/) on Github to start making your changes.
3. Write a test which shows that the bug was fixed or that the feature works as expected.
4. Send a pull request and bug the maintainer until it gets merged and published. :) Make sure to add yourself to CONTRIBUTORS.txt.