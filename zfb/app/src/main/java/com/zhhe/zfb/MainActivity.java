package com.zhhe.zfb;
        import android.os.Bundle;
        import android.os.Handler;
        import android.os.Message;
        import android.text.TextUtils;
        import android.widget.Toast;
        import com.alipay.sdk.app.PayTask;
        import com.unity3d.player.*;
        import java.util.Map;

public class MainActivity extends UnityPlayerActivity{
        private static final int SDK_PAY_FLAG=1;
        private static final String RESULT_SUCCESS = "9000";
        private static final String TIP_PAY_SUCCESS="支付成功";
        private static final String TIP_PAY_FAILED="支付失败";
        //支付回调:
        private Handler mHandler =new Handler(){
        public void handleMessage(Message msg){
        @SuppressWarnings("unchecked")
        PayResult payResult=new PayResult((Map<String,String>)
        msg.obj);
       //根据支付结果返回的参数进行判断,resultStatus:结果状态
        //如果resultStatus为9000则是支付成功,否则则判定为支付失败
        String resultStatus=payResult.getResultStatus();
        if(TextUtils.equals(resultStatus,RESULT_SUCCESS)){
        //支付成功
        Toast.makeText(MainActivity.this,TIP_PAY_SUCCESS,
        Toast.LENGTH_SHORT).show();
        }
        else{
        //支付失败
        Toast.makeText(MainActivity.this,TIP_PAY_FAILED,
        Toast.LENGTH_SHORT).show();
        }
        //Toast.makeText(MainActivity.this, payResult.getResult(),
        // Toast.LENGTH_LONG).show();
        }
        };
        @Override
        protected void onCreate(Bundle savedInstanceState){
        super.onCreate(savedInstanceState);
        }

//支付方法,提供给Unity调用,info参数是服务器加签后传递给客户端的最终请求参数.再由客户端调用该方法的时候传递.
    //方法体是官方提供调用支付宝SDK支付的代码.
            public void PayING(final String info){
        final String orderInfo=info;// 订单信息
        //支付代码
        Runnable payRunnable=new Runnable(){
            @Override
            public void run(){
                PayTask alipay=new PayTask(MainActivity.this);
                Map<String,String> result=alipay.payV2(orderInfo,true);
                //支付结果回调:就是执行了以上的mHandler
                Message msg=new Message();
                msg.what=SDK_PAY_FLAG;
                msg.obj=result;
                mHandler.sendMessage(msg);
                }
            };
        // 必须异步调用
        Thread payThread=new Thread(payRunnable);
        payThread.start();
        }
}