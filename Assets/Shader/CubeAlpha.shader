Shader "Custom/CubeAlpha"//Имя шейдеры при выборе его на материале
{
Properties //Блок с параметрами для Unity3D
{
  _MainTex ("Textures", 2D) = "white"{} //Имя параметра, описание для редактора и тип. Далее идёт значение по умолчанию 
  //Тип 2D указывает что тут обычная текстура
  _BumpMap ("Normal Map", 2D) = "bump" {}
  
  _SpecMap ("Specularmap", 2D) = "black" {}
  _SpecColor ("Specular Color", Color) = (0.5, 0.5, 0.5,1)
  _SpecPower ("Specular Power", Range(0,1)) = 0.5  
  
  _Cutoff ("Alpha Cutoff", Range(0,1)) = 0.0
  
}
  SubShader
  {
    //Tags{"RenderType" = "Opaque" } //Тэг RenderType = «Opaque» означает что мы собираемся отрисовать непрозрачный объект
    Tags{"RenderType" = "Opaque" "Opaque" = "AlphaTest" }
    LOD 200 //Уровень детализации (level of detail) в 3D приложениях
    //Pass { } определяет блок инструкций одного прохода    
    CGPROGRAM // вставка на языке CG
    #pragma surface surf BlinnPhong alphatest:_Cutoff
    //#pragma exclude_renderers flash
    //#pragma surface surf BlinnPhong
    // Объявление функции surface-шейдера и дополнительные параметры.
    // В данном случае функция называется surf, а в качестве дополнительных параметров указана модель освещения по Ламберту.        
    // еще бывает Lambert, BlinnPhong , Unlit или своя процедура    
    sampler2D _MainTex;
    sampler2D _BumpMap;
    sampler2D _SpecMap;
    float _SpecPower;
    
    struct Input //попросим у шейдера дополнительные переменные, которые нам понадобятся для расчетов в процедуре поверхности surf
    {
      float2 uv_BumpMap;
      float2 uv_MainTex; //(1.0, 1.0) U, V
      float2 uv_SpecMap;
      //UV-преобразование или развёртка представляет собой простой раскрой модели. https://ru.wikipedia.org/wiki/UV-%D0%BF%D1%80%D0%B5%D0%BE%D0%B1%D1%80%D0%B0%D0%B7%D0%BE%D0%B2%D0%B0%D0%BD%D0%B8%D0%B5
      //UV координаты, необходимые шейдеру для правильного наложения текстур на объект. Эти переменные обязательно должны называться так же, как и названия переменных текстур с префиксом uv_ или uv2_ для первого и второго каналов соответственно.
      //float4 color : COLOR; //(1.0, 1.0, 1.0, 1.0) R, G, B, A (red=100% green=100% blue=100% alpha=100%)
    };
    

    
    void surf(Input IN, inout SurfaceOutput o)
    {
      //o.Albedo = 1;
      fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
      fixed4 specTex = tex2D(_SpecMap, IN.uv_SpecMap);
      
      o.Albedo = tex.rgb;
      o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
      o.Specular = _SpecPower;
      o.Gloss = specTex.rgb;
      
      o.Alpha = tex.a;
      
    }
    ENDCG // конец вставки на языке CG  
  }
  Fallback "Diffuse"
}

//struct SurfaceOutput {
//    half3 Albedo; //Альбедо поверхности (Цвет)
//    half3 Normal; //Нормаль поверхности
//    half3 Emission; //Эмиссия (используется для расчета отражения)
//    half Specular; //"Размытие" отблеска в данной точке (зависит от направления камеры (dot(viewDir, Normal))
//    half Gloss; //Сила отблеска в данной точке
//    half Alpha; //Прозрачность в данной точке (не будет использоваться в "RenderType" = "Opaque")
//};

//Карта Bump описывает неровности материала, тёмные углубления, светлые выпуклости.
//Эффект достигается распределением светотени, геометрия не меняется.
//Изготавливается обычно из диффуза в фотошоп или аналогичных редакторах,
//в ряде случаев можно использовать и просто диффуз.
//
//Карта Normal map, также как и Bump описывает неровности материала, но по другому алгоритму.
//В карте нормалей, RGB картинка кодирует и XYZ и UVW координаты.
//Любой красный пиксель будет транслироваться в X координату, зеленый в Y, а синий в Z.
//Результат сводится к смеси этих трех цветов.
//Эффект достигается распределением светотени, геометрия не меняется.
//Карта Normal map обычно снимается с высокополигональной модели, для низкополигональной.
//
//Specular Map (карта отражения) – текстура, которая описывает способность отражения материала.
//Чем светлее , тем больше способность материала отражать свет и тем ярче на нём блики от света.
//Изготавливается обычно из диффуза в фотошоп или аналогичных редакторах.
//Это, всё, что нужно знать об этих картах, а также понимать свойства поверхности.