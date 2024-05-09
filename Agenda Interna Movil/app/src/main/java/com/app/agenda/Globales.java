package com.app.agenda;

import java.sql.Date;
import java.util.ArrayList;
import java.util.HashMap;

public class Globales {
    public static final String miIP = "192.168.0.5";
    public static final String verdadero = "true";
    public static final String protocolo = "http://";
    public static final String linkProyectosDelUsuario = "/agendainterna/servicios/proyectosdelusuario?idusuario=";
    public static final String linkDatosUsuario ="/agendainterna/servicios/datosusuario?correo=";
    public static final String linkDocumentosDelProyecto = "/agendainterna/servicios/documentosDelProyecto?idproyecto=";
    public static ArrayList<HashMap> list;

    private static String id;
    private static String nombre;
    private static String apellido;
    private static String tipousuario;
    private static String idproyecto;
    private static String nombreProyecto;

    public static String getId() {
        return id;
    }
    public static void setId(String id) {
        Globales.id = id;}

    public static String getNombre () {return nombre;}
    public static void setNombre(String nombre){
        Globales.nombre = nombre;}

    public static String getApellido(){return  apellido;}
    public static void setApellido(String apellido){
        Globales.apellido = apellido;}

    public static String getTipoUsuario(){return  tipousuario;}
    public static void setTipoUsuario(String tipoUsuario){
        Globales.tipousuario = tipoUsuario;}

    public static String getIdProyecto(){ return idproyecto;}
    public static void setIdproyecto(String idproyecto){
        Globales.idproyecto = idproyecto;
    }

    public static String getNombreProyecto(){ return nombreProyecto;}
    public static void setNombreProyecto(String nombreProyecto){
        Globales.nombreProyecto = nombreProyecto; }

    public static String FechaLeidaToString(String fecha){
        fecha=fecha.replace("/", "");
        fecha=fecha.replace("\\", "");
        fecha=fecha.replace("Date", "");
        fecha=fecha.replace(")", "");
        fecha=fecha.replace("(", "");
        Date ms = new Date(Long.parseLong(fecha));
        fecha=ms.toString();
        return fecha;
    }

}
