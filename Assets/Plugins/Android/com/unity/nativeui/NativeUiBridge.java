package com.unity.nativeui;

import android.app.Activity;
import android.graphics.Color;
import android.view.Gravity;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.LinearLayout;
import android.widget.ScrollView;
import android.widget.TextView;
import com.unity3d.player.UnityPlayer;
import org.json.JSONArray;
import org.json.JSONObject;

public class NativeUiBridge {

    private static ViewGroup currentOverlay;
    private static TextView statusLabel;

    // --- SCREEN 1: AD TYPE LIST ---
    public static void showAdTypes(String jsonList) {
        final Activity activity = UnityPlayer.currentActivity;
        activity.runOnUiThread(() -> {
            removeCurrentOverlay();

            LinearLayout mainLayout = new LinearLayout(activity);
            mainLayout.setOrientation(LinearLayout.VERTICAL);
            mainLayout.setBackgroundColor(Color.parseColor("#F2F2F2")); // Light gray

            // Title
            TextView header = new TextView(activity);
            header.setText("Select Ad Type");
            header.setTextSize(22);
            header.setGravity(Gravity.CENTER);
            header.setPadding(0, 50, 0, 30);
            header.setTextColor(Color.BLACK);
            mainLayout.addView(header);

            // Scrollable List
            ScrollView scrollView = new ScrollView(activity);
            LinearLayout listContainer = new LinearLayout(activity);
            listContainer.setOrientation(LinearLayout.VERTICAL);

            try {
                JSONObject obj = new JSONObject(jsonList);
                JSONArray arr = obj.getJSONArray("items");

                for (int i = 0; i < arr.length(); i++) {
                    JSONObject item = arr.getJSONObject(i);
                    String key = item.getString("key");
                    String adUnit = item.getString("adUnitName");

                    Button btn = new Button(activity);
                    btn.setText(key);
                    btn.setBackgroundColor(Color.WHITE);
                    btn.setTextColor(Color.BLACK);
                    btn.setTextAlignment(View.TEXT_ALIGNMENT_VIEW_START);
                    btn.setPadding(40, 30, 0, 30);
                    
                    // Maestro ID: "item_banner_gam"
                    btn.setContentDescription("item_" + adUnit);

                    btn.setOnClickListener(v -> {
                        UnityPlayer.UnitySendMessage("ScriptManager", "OnNativeSelection", adUnit);
                    });

                    LinearLayout.LayoutParams params = new LinearLayout.LayoutParams(
                            ViewGroup.LayoutParams.MATCH_PARENT, ViewGroup.LayoutParams.WRAP_CONTENT);
                    params.setMargins(0, 2, 0, 0); // Separator effect
                    listContainer.addView(btn, params);
                }
            } catch (Exception e) { e.printStackTrace(); }

            scrollView.addView(listContainer);
            mainLayout.addView(scrollView);

            addToWindow(mainLayout);
        });
    }

    // --- SCREEN 2: DETAIL / STATUS ---
    public static void showDetailScreen(String adUnitName, String initialStatus) {
        final Activity activity = UnityPlayer.currentActivity;
        activity.runOnUiThread(() -> {
            removeCurrentOverlay();

            LinearLayout layout = new LinearLayout(activity);
            layout.setOrientation(LinearLayout.VERTICAL);
            layout.setBackgroundColor(Color.WHITE);
            layout.setGravity(Gravity.TOP | Gravity.CENTER_HORIZONTAL);

            // 1. Back Button (Top Left logic, simplifed to top bar here)
            Button backBtn = new Button(activity);
            backBtn.setText("< Back");
            backBtn.setContentDescription("btn_back");
            backBtn.setOnClickListener(v -> UnityPlayer.UnitySendMessage("ScriptManager", "OnNativeBack", ""));
            LinearLayout.LayoutParams backParams = new LinearLayout.LayoutParams(
                ViewGroup.LayoutParams.MATCH_PARENT, ViewGroup.LayoutParams.WRAP_CONTENT);
            layout.addView(backBtn, backParams);

            // 2. Info Label
            TextView title = new TextView(activity);
            title.setText("Ad Unit: " + adUnitName);
            title.setTextSize(18);
            title.setGravity(Gravity.CENTER);
            title.setPadding(0, 50, 0, 20);
            layout.addView(title);

            // 3. Status Label (Dynamic)
            statusLabel = new TextView(activity);
            statusLabel.setText("Status: " + initialStatus);
            statusLabel.setTextSize(16);
            statusLabel.setTextColor(Color.DKGRAY);
            statusLabel.setGravity(Gravity.CENTER);
            statusLabel.setContentDescription("lbl_status");
            layout.addView(statusLabel);

            // 4. Spacer/Placeholder for Banners
            // Playwire banners usually attach to the window. 
            // We add a transparent view here just to push content if needed, 
            // but for bottom banners, they overlay naturally.
            
            addToWindow(layout);
        });
    }

    public static void updateStatus(String status) {
        final Activity activity = UnityPlayer.currentActivity;
        activity.runOnUiThread(() -> {
            if (statusLabel != null) {
                statusLabel.setText("Status: " + status);
            }
        });
    }

    // --- HELPERS ---
    private static void addToWindow(ViewGroup view) {
        currentOverlay = view;
        Activity act = UnityPlayer.currentActivity;
        act.addContentView(view, new ViewGroup.LayoutParams(
                ViewGroup.LayoutParams.MATCH_PARENT, 
                ViewGroup.LayoutParams.MATCH_PARENT));
    }

    private static void removeCurrentOverlay() {
        if (currentOverlay != null) {
            ViewGroup parent = (ViewGroup) currentOverlay.getParent();
            if (parent != null) parent.removeView(currentOverlay);
            currentOverlay = null;
            statusLabel = null;
        }
    }
}