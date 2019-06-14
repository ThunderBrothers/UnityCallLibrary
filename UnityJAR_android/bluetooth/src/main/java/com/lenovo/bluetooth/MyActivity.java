package com.lenovo.bluetooth;

import android.app.AlertDialog;
import android.content.Intent;
import com.unity3d.player.UnityPlayerActivity;
/**
 * Created by Administrator on 2018/4/13.
 */
public class MyActivity extends UnityPlayerActivity {
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
