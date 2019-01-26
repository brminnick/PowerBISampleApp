# PowerBISampleApp

A sample app demonstrating how to interact with a PowerBI backend form a Xamarin.Forms app

To use this sample, you will need to create a native app registration for your Azure AD instance.

Here is the configuration of the Native App Registration that is being used for this sample.

It is also where you'll find the `ApplicationId` and `RedirectUrl` for your Native App Registration.

## Azure AD App Registration
(Make sure the Application Type is Native)

![App Registration](https://user-images.githubusercontent.com/13558917/51790136-4138fa00-2189-11e9-812a-b6ccd1d0c981.png)

## ApplicationID
This is the value you'll use for [`AzureConstants.ApplicationId`](blob/master/PowerBISampleApp/Constants/AzureConstants.cs)

![ApplicationId](https://user-images.githubusercontent.com/13558917/51790135-40a06380-2189-11e9-80d3-a4974b1d6d45.png)

This is the value you'll use for [`AzureConstants.RedirectURL`](blob/master/PowerBISampleApp/Constants/AzureConstants.cs)

![RedirectUrl](https://user-images.githubusercontent.com/13558917/51790137-4138fa00-2189-11e9-9c0c-5e685f1a9771.png)
