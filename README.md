# 👋 Introduction
**Topic: Predict the closing price**  
**Idea:** Use the opening-closing price percentage of the previous 12 candles to predict the opening-closing price percentage of the current candle. From there, the closing price can be calculated.  
**Demo:** [https://ai4e-hoapham.azurewebsites.net/](https://ai4e-hoapham.azurewebsites.net/)  


# 💡 Solution  

![Solution](https://github.com/HoaQB208/AI4E-FinalProject/assets/32737501/425f09a6-4bee-4b6a-8ab6-9725b14db3a9)


# 🗂 DataSet  
**Download trading data:**  
Download trading data on exchanges like Binance. The received data includes **OpenTime, O-H-L-C prices, Volume**.  

![image](https://github.com/HoaQB208/AI4E-FinalProject/assets/32737501/bc556810-0eca-49ea-b7ab-87579d33aae6)  

Trading data is saved in Json format but basically if presented in tabular form it will be as follows:  
| D | O | H | L | C | V |
|-------|-------|-------|-------|-------|-------|
| 2023-11-09T00 | 35626.7 | 35900.0 | 35538.8 | 35866.2 | 20513.56 |
| 2023-11-09T01 | 35866.1 | 36100.0 | 35773.2 | 35914.1 | 21092.22 |
| 2023-11-09T02 | 35914.0 | 36888.0 | 35851.2 | 36382.5 | 55951.32 |
...  


**Extract features:**  
- In this project only the **Closing and Opening prices** are used.  
- Calculate the percentage of closing price compared to opening price of each candle (Negative number when closing price is lower than opening price and vice versa).  
**Percent**  
0.054  
-0.111  
-0.834  
0.032  
-0.190  
0.098  
0.245  
-0.008  
...  
- Create features and labels. With 12 percents being features, the next percent is Label.
  
![DataSet](https://github.com/HoaQB208/AI4E-FinalProject/assets/32737501/165e4782-600d-4f6d-aa1d-96a54674b9b6)  

| C1 | C2 | C3 | C4 | C5 | C6 | C7 | C8 | C9 | 10 | C11 | C12 | Label |
|----------|----------|----------|----------|----------|----------|----------|----------|----------|-----------|------------|------------|------------|
| 0.054   | -0.111  | -0.834  | 0.032   | -0.190  | 0.098   | 0.245   | -0.008  | -0.817  | 0.215    | 1.675     | -0.201    | -0.162    |
| -0.111  | -0.834  | 0.032   | -0.190  | 0.098   | 0.245   | -0.008  | -0.817  | 0.215   | 1.675    | -0.201    | -0.162    | 1.333     |
| -0.834  | 0.032   | -0.190  | 0.098   | 0.245   | -0.008  | -0.817  | 0.215   | 1.675   | -0.201   | -0.162    | 1.333     | -0.155    |
...  

  
![image](https://github.com/HoaQB208/AI4E-FinalProject/assets/32737501/08ab6d9c-994c-4f60-a6ab-a270512f2322)  



# ✨ Training  
Solution use **ML.NET** library.  
+ Select Model Type as **Regression**.  
+ Choose a specific Algorithm (eg: FastForest...) or use **AutoML** to automatically find the optimal model.  
+ Optimize the model according to **RMSE**.  
+ After the maximum time allowed or the training process ends, AutoML will choose the **best model** that has been trained.  
+ The model is saved to the **Model.zip** file.  
+ The Model.zip file will be placed on the **Server** and wait for the **Client** to call.
  
![image](https://github.com/HoaQB208/AI4E-FinalProject/assets/32737501/57b034dd-12ed-4f2a-ae76-1fd9d9ed4cbf)



# ⛳️ Using  
Closing price prediction for:  
+ Market: **Futures**  
+ Exchanges: **Binance**  
+ Interval: **H1**  
+ URL: **[https://ai4e-hoapham.azurewebsites.net/](https://ai4e-hoapham.azurewebsites.net/)**  

![image](https://github.com/HoaQB208/AI4E-FinalProject/assets/32737501/bbcdfb31-f5e5-4eed-a439-2482cb9158e6)
