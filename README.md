# code-challenge

Created new branch under name (LuckDayCodeChallenge), it contains below items

solution: LuckDayCodeChallenge.sln
    the solution contains 3 projects. Please ignore (LuckDayCodeChallenge) I will remove it later
    1. WebAPI (Main Web API)
    2. WebAPITests (Unit Test for the Web API)
    3. Common (Class library that has common used classes or reusable)
    4. LuckDayCodeChallenge (ignore it please)
    
    I created the common project to contain all reusable classes, the idea is like having framework that you can use it in any project. example of the classes:
    A. HttpProvider to integrate with any 3rd party API
    B. MemoryCacheHelper for caching (will explain at the end why I used normal MemoryCache)
    C. DataHelper for any kind of data process like removing special chars from string
    
    WebAPI is the main project, please make sure to select it as (Start Project)
    Simple REST API that has 1 resouce (Feeds) and it's inherited (BaseController) that will be responsible for all common used methods in all controllers.
    
    Feeds controller has 1 action for now which is Get that accept
        A. q parameter as keywork and you can pass comma seprated between the keywords
        B. index (Optional) as page index that starts from zero
        C. size (Optional) the page size with default of 10 items
        D. refresh (Optional) is a boolean parameter to refresh the cache
        
    Feeds/Get response looks like
        A. title
        B. media
        C. published
        
    Feeds controller is getting feed from IFlickrApiProvider that is responsible for:
        A. Prepare the requset query
        B. Call Flickr API using (IExternalApiRequestHelper) as wrapper for (HttpProvider)
        C. Cache the response to use it in the future for easy access and better performance
        D. Parse the response to Flickr model
        
    Also created global exception filter to catch all API exceptions and handle them by loggin the exception and swallow it to pretect sensitive information from API user and return 500 http code.
    
    *Note: logging not implemented
    
    WebAPITests is a Unit Test for the WebAPI and I only created 1 senario for now.
        Senario #1: pass single user Id as keyword and check if it's null or not.
    
    I tried to keep it simple without adding extra complexity, and made it easy to be expandable. Also as I mentioned before the I used MemoryCache instead of other advanced caching like Redis for the lack of time.
    
    
    Suggestions:
    
        Adding more resources and actions by using more service from Flickr API
        Use advanced caching for bigger data and better perfomance
        Adding more test senarios
        Make the WebAPI secure by using OAuth2
        


