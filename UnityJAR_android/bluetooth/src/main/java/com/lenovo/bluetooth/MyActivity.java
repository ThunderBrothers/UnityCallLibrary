package com.lenovo.bluetooth;

import android.Manifest;
import android.app.AlertDialog;
import android.bluetooth.BluetoothAdapter;
import android.bluetooth.BluetoothDevice;
import android.bluetooth.BluetoothManager;
import android.bluetooth.le.BluetoothLeScanner;
import android.bluetooth.le.ScanCallback;
import android.bluetooth.le.ScanResult;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.support.v4.app.ActivityCompat;
import android.support.v4.content.ContextCompat;
import android.util.Log;
import android.os.Handler;
import android.widget.Toast;

import com.unity3d.player.UnityPlayer;
import com.unity3d.player.UnityPlayerActivity;

//import java.util.logging.Handler;

import java.util.List;

import static android.content.ContentValues.TAG;

/**
 * Created by Administrator on 2018/4/13.
 */
public class MyActivity extends UnityPlayerActivity {

    private BluetoothAdapter mBluetoothAdapter;
    private boolean isScanning = false;
    private Handler mHandler= new Handler();
    private static final long SCAN_PERIOD = 15000;
    private static String unityCallingManagerObjName = "BluetoothManager";
    private static String unityFuncName = "CalledFilter";
    private List<BluetoothDevice> devices;
    private static final int PERMISSION_REQUEST_COARSE_LOCATION = 1;

    //初始化Adapter,申请该权限
    public void getBluetoothAdapter(){
        Log.d(TAG, "[JAR] 申请该权限 ");
        /*
        if (ContextCompat.checkSelfPermission(this,Manifest.permission.ACCESS_COARSE_LOCATION)!= PackageManager.PERMISSION_GRANTED){
            ActivityCompat.requestPermissions(this,new String[]{Manifest.permission.ACCESS_COARSE_LOCATION},PERMISSION_REQUEST_COARSE_LOCATION);
            if(ActivityCompat.shouldShowRequestPermissionRationale(this,
                    Manifest.permission.READ_CONTACTS)) {
                Toast.makeText(this,"申请该权限",
                        Toast.LENGTH_SHORT).show();
            }
        }
        */
        Log.d(TAG, "[JAR] 初始化Adapter ");
        if (mBluetoothAdapter == null){
            //final BluetoothManager bluetoothManager = (BluetoothManager) getSystemService(BLUETOOTH_SERVICE);
            //        BluetoothAdapter mBluetoothAdapter = bluetoothManager.getAdapter();
            final BluetoothManager bluetoothManager = (BluetoothManager) getSystemService(BLUETOOTH_SERVICE);
            mBluetoothAdapter = bluetoothManager.getAdapter();
        }
    }
    //申请该权限回调
    @Override
    public void onRequestPermissionsResult(int requestCode, String permissions[], int[] grantResults) {
        Log.d(TAG, "[JAR] 申请该权限回调");
        switch (requestCode) {
            case PERMISSION_REQUEST_COARSE_LOCATION:
                if (grantResults[0] == PackageManager.PERMISSION_GRANTED) {
                    Log.d(TAG, "[JAR] 申请该权限 PERMISSION_REQUEST_COARSE_LOCATION");
                }
                break;
        }
    }
    //判断是否有蓝牙功能模块
    public boolean hasBluetooth(){
        Log.d(TAG, "[JAR] 判断是否有蓝牙功能模块 ");
        boolean hasBluetooth = false;
        if (getPackageManager().hasSystemFeature(PackageManager.FEATURE_BLUETOOTH_LE)){
            hasBluetooth = true;
        }
        return hasBluetooth;
    }



    //设备是否支持蓝牙
    public boolean isSupportBlue(){
        Log.d(TAG, "[JAR] 设备是否支持蓝牙");
        return mBluetoothAdapter != null;
    }
    //蓝牙是否打开
    public boolean isBlueEnable(){
        Log.d(TAG, "[JAR] 查看蓝牙打开状态: ");
        return isSupportBlue() && mBluetoothAdapter.isEnabled();
    }
    //打开蓝牙（同步）
    //弹出打开蓝牙提示
    public void opneBlueSync(){
        Log.d(TAG, "[JAR] 弹框打开蓝牙 ");
        Intent intent = new Intent(BluetoothAdapter.ACTION_REQUEST_ENABLE);
        startActivityForResult(intent,0xa01);
    }
    //自动打开蓝牙（异步：蓝牙不会立刻就处于开启状态）
    //这个方法打开蓝牙不会弹出提示
    public void openBlueAsyn(){
        Log.d(TAG, "[JAR] 打开蓝牙");
        if (isSupportBlue()){
            mBluetoothAdapter.enable();
        }
    }
    //扫描周围蓝牙设备（配对上的设备有可能扫描不出来）
    public void scanDevice(final boolean enable){
        Log.d(TAG, "[JAR] 扫描周围蓝牙设备" + enable);
        if (enable){
            mHandler.postDelayed(new Runnable() {
                @Override
                public void run() {
                    isScanning = false;
                    BluetoothLeScanner mBLEScnner = mBluetoothAdapter.getBluetoothLeScanner();
                    mBLEScnner.stopScan(mScanCallback);
                    Log.d(TAG, "[JAR] 时间到结束扫描");
                }
            },SCAN_PERIOD);
            isScanning = true;
            BluetoothLeScanner mBLEScnner = mBluetoothAdapter.getBluetoothLeScanner();
            mBLEScnner.startScan(mScanCallback);
            Log.d(TAG, "[JAR] 开始扫描");
        }else {
            isScanning = false;
            BluetoothLeScanner mBLEScnner = mBluetoothAdapter.getBluetoothLeScanner();
            mBLEScnner.stopScan(mScanCallback);
            Log.d(TAG, "[JAR] 结束扫描");
        }
    }
    //扫描回调
    private ScanCallback mScanCallback = new ScanCallback() {
        @Override
        public void onScanResult(int callbackType, ScanResult result){
            super.onScanResult(callbackType,result);
            Log.d(TAG,"[JAR]搜索回调");
            BluetoothDevice device = result.getDevice();
            Log.d(TAG,"[JAR] device =" + device);
            if (!devices.contains(device)){
                devices.add(device);
            }
            callUnityFunc(device.getName());
        }
        @Override
        public void onBatchScanResults(List<ScanResult> results) {
            super.onBatchScanResults(results);
            Log.d(TAG,"[JAR]批量返回扫描结果 count =" + results.size());
        }
        @Override
        public void onScanFailed(int errorCode) {
            super.onScanFailed(errorCode);
            Log.d(TAG,"[JAR]搜索失败");
        }

    };

    /*
    private ScanCallback mLeScanCallback = new ScanCallback() {
        public void onLeScan(final BluetoothDevice device, int rssi, byte[] scanRecord) {
            Log.d(TAG, "[JAR] 扫描回调");
            //这里是个子线程，下面把它转换成主线程处理
            runOnUiThread(new Runnable() {
                @Override
                public void run() {
                    Log.d(TAG, "[JAR] 扫描getName" +device.getName());
                    callUnityFunc(device.getName());
                    Log.d(TAG, "[JAR] 扫描getAddress" +device.getAddress());
                    callUnityFunc(device.getAddress());
                    //在这里可以把搜索到的设备保存起来
                    //device.getName();获取蓝牙设备名字
                    //device.getAddress();获取蓝牙设备mac地址
                    //这里的rssi即信号强度，即手机与设备之间的信号强度。
                }
            });
        }
    };
    */

    //安卓调用Unity
   private void callUnityFunc(String filterStr){
       Log.d(TAG, "[JAR] 安卓调用Unity 筛选器值 =" + filterStr);
       UnityPlayer.UnitySendMessage(unityCallingManagerObjName,unityFuncName,filterStr);
   }


    public void showDialog(final String mTitle, final String mContent) {
        runOnUiThread(new Runnable() {

            @Override
            public void run() {
                AlertDialog.Builder mBuilder = new AlertDialog.Builder(MyActivity.this);
                mBuilder.setTitle(mTitle)
                        .setMessage(mContent)
                        .setPositiveButton("确定", null);
                mBuilder.show();
            }
        });
    }

    //创建一个分享
    public void shareText(String subject, String body) {
        Intent sharingIntent = new Intent(android.content.Intent.ACTION_SEND);//创建一个分享
        sharingIntent.setType("text/plain");
        sharingIntent.putExtra(android.content.Intent.EXTRA_SUBJECT, subject);//设置分享类型
        sharingIntent.putExtra(android.content.Intent.EXTRA_TEXT, body);//设置分享内容
        startActivity(Intent.createChooser(sharingIntent, "Share via"));//分享标题
    }

    public int GetAdd(int numA,int numB) {
        return (numA + numB);
    }
}
