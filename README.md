# PowerBISampleApp

A sample app demonstrating how to interact with a PowerBI backend form a Xamarin.Forms app

## To Do

To use this sample, you will need to create a native app registration for your Azure AD instance.

Below are the screenshots for the Native App Registration that is being used for this sample. It is where you'll find the `ApplicationId` and `RedirectUrl` for your Native App Registration.

For the complete walkthrough on how to embed PowerBI into your app, follow these steps: ["Tutorial: Embed a Power BI report, dashboard, or tile into an application for your customers"](https://docs.microsoft.com/power-bi/developer/embed-sample-for-customers?WT.mc_id=mobile-0000-bramin).

### Azure AD App Registration

In Azure AD, create follow the App Registration process to create a **Native** Application Type

![App Registration](https://user-images.githubusercontent.com/13558917/51790136-4138fa00-2189-11e9-812a-b6ccd1d0c981.png)

### Azure AD App, API Permissions

In the Native Application created in Azure AD, select **API permissions** and add permissions for Power BI Services

![API Permissions](https://user-images.githubusercontent.com/13558917/58598341-c401bc80-8230-11e9-8a91-e8158b801816.png)

### ApplicationID

This is the value you'll use for [`AzureConstants.ApplicationId`](/PowerBISampleApp/Constants/AzureConstants.cs)

![ApplicationId](https://user-images.githubusercontent.com/13558917/51790135-40a06380-2189-11e9-80d3-a4974b1d6d45.png)

### Redirect URL

This is the value you'll use for [`AzureConstants.RedirectURL`](/PowerBISampleApp/Constants/AzureConstants.cs)

![RedirectUrl](https://user-images.githubusercontent.com/13558917/51790137-4138fa00-2189-11e9-9c0c-5e685f1a9771.png)
