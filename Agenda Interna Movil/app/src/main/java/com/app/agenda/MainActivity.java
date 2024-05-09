package com.app.agenda;

import androidx.annotation.RequiresApi;
import androidx.appcompat.app.AppCompatActivity;

import android.app.Activity;
import android.os.Bundle;
import android.widget.Button;
import android.widget.TextView;



import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonArrayRequest;
import com.android.volley.toolbox.JsonObjectRequest;
import com.android.volley.toolbox.Volley;

import android.content.Context;
import android.content.Intent;
import android.os.Build;
import android.os.Bundle;
import android.util.JsonToken;
import android.view.View;
import android.view.inputmethod.InputMethodManager;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;
import android.widget.Toolbar;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

public class MainActivity extends Activity {

    private TextView textViewClave;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        textViewClave = (TextView) findViewById(R.id.textview_clave);
        Button btn = (Button) findViewById(R.id.btn_login);
    }

    @Override
    protected void onResume() {
        super.onResume();
        if (!(Globales.getId()==null)) {
            Intent intent = new Intent(getApplicationContext(), Proyectos.class);
            intent.putExtra("id", Globales.getId());
            intent.putExtra("nombre", Globales.getNombre());
            intent.putExtra("apellido", Globales.getApellido());
            startActivity(intent);
        }
        EditText edtlogin = (EditText) findViewById(R.id.edt_login_correo);
        EditText edtclave = (EditText) findViewById(R.id.edt_login_password);

        if (!(edtclave.getText().toString().isEmpty()) && !(edtlogin.getText().toString().isEmpty())){
            edtlogin.setText("");
            edtclave.setText("");
        }
    }



    @RequiresApi(api = Build.VERSION_CODES.M)
    public void chequealogin(View view){

        // para contraer teclado al hacer click
        try {
            View v = this.getCurrentFocus();
            v.clearFocus();
            if (v != null) {
                InputMethodManager imm = (InputMethodManager) getSystemService(Context.INPUT_METHOD_SERVICE);
                imm.hideSoftInputFromWindow(v.getWindowToken(), 0);
            }
        }catch (Exception e){}

        //

        EditText edtcorreo = (EditText) findViewById(R.id.edt_login_correo);
        EditText edtclave = (EditText) findViewById(R.id.edt_login_password);
        String correo = edtcorreo.getText().toString();
        String pass = edtclave.getText().toString();

        if(correo!="" && pass!=""){
            // generar hash para consulta
            pass = hashClave(pass);
            String URL = Globales.protocolo+ Globales.miIP+Globales.linkDatosUsuario+correo+"&clave="+pass;
            JsonArrayRequest request;
            RequestQueue requestQueue;
            JsonObjectRequest jsonObjectRequest = new JsonObjectRequest
                    (Request.Method.POST, URL, null, new Response.Listener<JSONObject>() {
                        @Override
                        public void onResponse(JSONObject response) {

                            JSONObject jsonObject = null;
                            try {
                                Intent intent = new Intent(getApplicationContext(),Proyectos.class);

                                Globales.setId(response.getString("idusuario"));
                                Globales.setNombre(response.getString("nombre"));
                                Globales.setApellido(response.getString("apellido"));
                                Globales.setTipoUsuario(response.getString("idtipousuario"));

                                startActivity(intent);
                            } catch (JSONException e) {
                                e.printStackTrace();
                                Toast.makeText(getApplicationContext(), "usuario no registrado", Toast.LENGTH_LONG).show();
                            }
                        }
                    }, new Response.ErrorListener() {
                        @Override
                        public void onErrorResponse(VolleyError error) {
                            error.printStackTrace();
                            Toast.makeText(getApplicationContext(), "usuario o password incorrectos", Toast.LENGTH_LONG).show();
                        }
                    });

            requestQueue = Volley.newRequestQueue(this);
            requestQueue.add(jsonObjectRequest);

        }else{
            Toast.makeText(getApplicationContext(), "Debe indicar una usuario y password", Toast.LENGTH_LONG).show();
        }
    }

    public void applyTexts(String clave) {
        Toast.makeText(this, "Se enviará su nueva CONTRASEÑA al correo:"+clave, Toast.LENGTH_LONG).show();
    }

    private String hashClave(String clave) {
        try { java.security.MessageDigest md = java.security.MessageDigest.getInstance("md5");
            byte[] array = md.digest(clave.getBytes());
            StringBuffer sb = new StringBuffer();
            for (int i = 0; i < array.length; ++i) {
                sb.append(Integer.toHexString((array[i] & 0xFF) | 0x100).substring(1,3));
            }
            return sb.toString();
        } catch (java.security.NoSuchAlgorithmException e) {

        }
        return null;
    }

}