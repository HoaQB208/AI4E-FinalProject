# 👋 Introduction
**Topic: Predict the closing price**  
**Idea:** Use the opening-closing price percentage of the previous 12 candles to predict the opening-closing price percentage of the current candle. From there, the closing price can be calculated.  
**Demo:** [https://ai4e-hoapham.azurewebsites.net/](https://ai4e-hoapham.azurewebsites.net/)  


# 💡 Solution
![image](https://github.com/HoaQB208/AI4E-FinalProject/assets/32737501/45802994-a650-46e1-9123-242ff51c73ae)



# 🗂 DataSet
**Download trading data:**  
Download trading data on exchanges like Binance. The received data includes OpenTime, O-H-L-C prices, Volume.  
![image](https://github.com/HoaQB208/AI4E-FinalProject/assets/32737501/bc556810-0eca-49ea-b7ab-87579d33aae6)



**Extract features:**  
- Calculate the percentage of closing price compared to opening price of each candle (Negative number when closing price is lower than opening price and vice versa).  
- Create features and labels. With 12 percent being features, the next percent is Label.
- Save to json file.  

![image](https://github.com/HoaQB208/AI4E-FinalProject/assets/32737501/08ab6d9c-994c-4f60-a6ab-a270512f2322)



# ✨ Training  
Solution use **ML.NET** library.  
+ Select Model Type as **Regression**.  
+ Choose a specific Algorithm or use **AutoML** to automatically find the optimal model.  
+ Optimize the model according to **RMSE**.  
+ After the maximum time allowed, AutoML will choose the **best model** that has been trained.  
+ The model is saved to the **Model.zip** file.  
+ The Model.zip file will be placed on the **Server** and wait for the **Client** to call.
  
![image](https://github.com/HoaQB208/AI4E-FinalProject/assets/32737501/57b034dd-12ed-4f2a-ae76-1fd9d9ed4cbf)



# ⛳️ Using
Closing price prediction for:  
**Futures** market on **Binance**
Time frame: **H1**
**URL:** [https://ai4e-hoapham.azurewebsites.net/](https://ai4e-hoapham.azurewebsites.net/)  
