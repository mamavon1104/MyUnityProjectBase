using System.Collections.Generic;
using UnityEngine;

namespace Mamavon.Funcs
{
#if false　//我が講師のCPPからお借りしました
    static constexpr int  AliceBlue = TNL_ARGB8(240, 248, 255, 255);
    static constexpr int  AntiqueWhite = TNL_ARGB8(250, 235, 215, 255);
    static constexpr int  Aqua = TNL_ARGB8(0, 255, 255, 255);
    static constexpr int  Aquamarine = TNL_ARGB8(127, 255, 212, 255);
    static constexpr int  Azure = TNL_ARGB8(240, 255, 255, 255);
    static constexpr int  Beige = TNL_ARGB8(245, 245, 220, 255);
    static constexpr int  Bisque = TNL_ARGB8(255, 228, 196, 255);
    static constexpr int  Black = TNL_ARGB8(0, 0, 0, 255);
    static constexpr int  BlanchedAlmond = TNL_ARGB8(255, 235, 205, 255);
    static constexpr int  Blue = TNL_ARGB8(0, 0, 255, 255);
    static constexpr int  BlueViolet = TNL_ARGB8(138, 43, 226, 255);
    static constexpr int  Brown = TNL_ARGB8(165, 42, 42, 255);
    static constexpr int  Burlywood = TNL_ARGB8(222, 184, 135, 255);
    static constexpr int  CadetBlue = TNL_ARGB8(95, 158, 160, 255);
    static constexpr int  Chartreuse = TNL_ARGB8(127, 255, 0, 255);
    static constexpr int  Chocolate = TNL_ARGB8(210, 105, 30, 255);
    static constexpr int  Coral = TNL_ARGB8(255, 127, 80, 255);
    static constexpr int  CornflowerBlue = TNL_ARGB8(100, 149, 237, 255);
    static constexpr int  Cornsilk = TNL_ARGB8(255, 248, 220, 255);
    static constexpr int  Crimson = TNL_ARGB8(220, 20, 60, 255);
    static constexpr int  Cyan = TNL_ARGB8(0, 255, 255, 255);
    static constexpr int  DarkBlue = TNL_ARGB8(0, 0, 139, 255);
    static constexpr int  DarkCyan = TNL_ARGB8(0, 139, 139, 255);
    static constexpr int  DarkGoldenrod = TNL_ARGB8(184, 134, 11, 255);
    static constexpr int  DarkGray = TNL_ARGB8(169, 169, 169, 255);
    static constexpr int  DarkGreen = TNL_ARGB8(0, 100, 0, 255);
    static constexpr int  DarkKhaki = TNL_ARGB8(189, 183, 107, 255);
    static constexpr int  DarkMagenta = TNL_ARGB8(139, 0, 139, 255);
    static constexpr int  DarkOliveGreen = TNL_ARGB8(85, 107, 47, 255);
    static constexpr int  DarkOrange = TNL_ARGB8(255, 140, 0, 255);
    static constexpr int  DarkOrchid = TNL_ARGB8(153, 50, 204, 255);
    static constexpr int  DarkRed = TNL_ARGB8(139, 0, 0, 255);
    static constexpr int  DarkSalmon = TNL_ARGB8(233, 150, 122, 255);
    static constexpr int  DarkSeaGreen = TNL_ARGB8(143, 188, 143, 255);
    static constexpr int  DarkSlateBlue = TNL_ARGB8(72, 61, 139, 255);
    static constexpr int  DarkSlateGray = TNL_ARGB8(47, 79, 79, 255);
    static constexpr int  DarkTurquoise = TNL_ARGB8(0, 206, 209, 255);
    static constexpr int  DarkViolet = TNL_ARGB8(148, 0, 211, 255);
    static constexpr int  DeepPink = TNL_ARGB8(255, 20, 147, 255);
    static constexpr int  DeepSkyBlue = TNL_ARGB8(0, 191, 255, 255);
    static constexpr int  DimGray = TNL_ARGB8(105, 105, 105, 255);
    static constexpr int  DodgerBlue = TNL_ARGB8(30, 144, 255, 255);
    static constexpr int  FireBrick = TNL_ARGB8(178, 34, 34, 255);
    static constexpr int  FloralWhite = TNL_ARGB8(255, 250, 240, 255);
    static constexpr int  ForestGreen = TNL_ARGB8(34, 139, 34, 255);
    static constexpr int  Fuchsia = TNL_ARGB8(255, 0, 255, 255);
    static constexpr int  Gainsboro = TNL_ARGB8(220, 220, 220, 255);
    static constexpr int  GhostWhite = TNL_ARGB8(248, 248, 255, 255);
    static constexpr int  Gold = TNL_ARGB8(255, 215, 0, 255);
    static constexpr int  Goldenrod = TNL_ARGB8(218, 165, 32, 255);
    static constexpr int  Gray = TNL_ARGB8(128, 128, 128, 255);
    static constexpr int  Green = TNL_ARGB8(0, 128, 0, 255);
    static constexpr int  GreenYellow = TNL_ARGB8(173, 255, 47, 255);
    static constexpr int  Honeydew = TNL_ARGB8(240, 255, 240, 255);
    static constexpr int  HotPink = TNL_ARGB8(255, 105, 180, 255);
    static constexpr int  IndianRed = TNL_ARGB8(205, 92, 92, 255);
    static constexpr int  Indigo = TNL_ARGB8(75, 0, 130, 255);
    static constexpr int  Ivory = TNL_ARGB8(255, 255, 240, 255);
    static constexpr int  Khaki = TNL_ARGB8(240, 230, 140, 255);
    static constexpr int  Lavender = TNL_ARGB8(230, 230, 250, 255);
    static constexpr int  Lavenderblush = TNL_ARGB8(255, 240, 245, 255);
    static constexpr int  LawnGreen = TNL_ARGB8(124, 252, 0, 255);
    static constexpr int  LemonChiffon = TNL_ARGB8(255, 250, 205, 255);
    static constexpr int  LightBlue = TNL_ARGB8(173, 216, 230, 255);
    static constexpr int  LightCoral = TNL_ARGB8(240, 128, 128, 255);
    static constexpr int  LightCyan = TNL_ARGB8(224, 255, 255, 255);
    static constexpr int  LightGoldenodYellow = TNL_ARGB8(250, 250, 210, 255);
    static constexpr int  LightGray = TNL_ARGB8(211, 211, 211, 255);
    static constexpr int  LightGreen = TNL_ARGB8(144, 238, 144, 255);
    static constexpr int  LightPink = TNL_ARGB8(255, 182, 193, 255);
    static constexpr int  LightSalmon = TNL_ARGB8(255, 160, 122, 255);
    static constexpr int  LightSeaGreen = TNL_ARGB8(32, 178, 170, 255);
    static constexpr int  LightSkyBlue = TNL_ARGB8(135, 206, 250, 255);
    static constexpr int  LightSlateGray = TNL_ARGB8(119, 136, 153, 255);
    static constexpr int  LightSteelBlue = TNL_ARGB8(176, 196, 222, 255);
    static constexpr int  LightYellow = TNL_ARGB8(255, 255, 224, 255);
    static constexpr int  Lime = TNL_ARGB8(0, 255, 0, 255);
    static constexpr int  LimeGreen = TNL_ARGB8(50, 205, 50, 255);
    static constexpr int  Linen = TNL_ARGB8(250, 240, 230, 255);
    static constexpr int  Magenta = TNL_ARGB8(255, 0, 255, 255);
    static constexpr int  Maroon = TNL_ARGB8(128, 0, 0, 255);
    static constexpr int  MediumAquamarine = TNL_ARGB8(102, 205, 170, 255);
    static constexpr int  MediumBlue = TNL_ARGB8(0, 0, 205, 255);
    static constexpr int  MediumOrchid = TNL_ARGB8(186, 85, 211, 255);
    static constexpr int  MediumPurple = TNL_ARGB8(147, 112, 219, 255);
    static constexpr int  MediumSeaGreen = TNL_ARGB8(60, 179, 113, 255);
    static constexpr int  MediumSlateBlue = TNL_ARGB8(123, 104, 238, 255);
    static constexpr int  MediumSpringGreen = TNL_ARGB8(0, 250, 154, 255);
    static constexpr int  MediumTurquoise = TNL_ARGB8(72, 209, 204, 255);
    static constexpr int  MediumVioletRed = TNL_ARGB8(199, 21, 133, 255);
    static constexpr int  MidnightBlue = TNL_ARGB8(25, 25, 112, 255);
    static constexpr int  Mintcream = TNL_ARGB8(245, 255, 250, 255);
    static constexpr int  MistyRose = TNL_ARGB8(255, 228, 225, 255);
    static constexpr int  Moccasin = TNL_ARGB8(255, 228, 181, 255);
    static constexpr int  NavajoWhite = TNL_ARGB8(255, 222, 173, 255);
    static constexpr int  Navy = TNL_ARGB8(0, 0, 128, 255);
    static constexpr int  OldLace = TNL_ARGB8(253, 245, 230, 255);
    static constexpr int  Olive = TNL_ARGB8(128, 128, 0, 255);
    static constexpr int  Olivedrab = TNL_ARGB8(107, 142, 35, 255);
    static constexpr int  Orange = TNL_ARGB8(255, 165, 0, 255);
    static constexpr int  Orangered = TNL_ARGB8(255, 69, 0, 255);
    static constexpr int  Orchid = TNL_ARGB8(218, 112, 214, 255);
    static constexpr int  PaleGoldenrod = TNL_ARGB8(238, 232, 170, 255);
    static constexpr int  PaleGreen = TNL_ARGB8(152, 251, 152, 255);
    static constexpr int  PaleTurquoise = TNL_ARGB8(175, 238, 238, 255);
    static constexpr int  PaleVioletred = TNL_ARGB8(219, 112, 147, 255);
    static constexpr int  PapayaWhip = TNL_ARGB8(255, 239, 213, 255);
    static constexpr int  PeachPuff = TNL_ARGB8(255, 218, 185, 255);
    static constexpr int  Peru = TNL_ARGB8(205, 133, 63, 255);
    static constexpr int  Pink = TNL_ARGB8(255, 192, 203, 255);
    static constexpr int  Plum = TNL_ARGB8(221, 160, 221, 255);
    static constexpr int  PowderBlue = TNL_ARGB8(176, 224, 230, 255);
    static constexpr int  Purple = TNL_ARGB8(128, 0, 128, 255);
    static constexpr int  Red = TNL_ARGB8(255, 0, 0, 255);
    static constexpr int  RosyBrown = TNL_ARGB8(188, 143, 143, 255);
    static constexpr int  RoyalBlue = TNL_ARGB8(65, 105, 225, 255);
    static constexpr int  SaddleBrown = TNL_ARGB8(139, 69, 19, 255);
    static constexpr int  Salmon = TNL_ARGB8(250, 128, 114, 255);
    static constexpr int  SandyBrown = TNL_ARGB8(244, 164, 96, 255);
    static constexpr int  SeaGreen = TNL_ARGB8(46, 139, 87, 255);
    static constexpr int  Seashell = TNL_ARGB8(255, 245, 238, 255);
    static constexpr int  Sienna = TNL_ARGB8(160, 82, 45, 255);
    static constexpr int  Silver = TNL_ARGB8(192, 192, 192, 255);
    static constexpr int  SkyBlue = TNL_ARGB8(135, 206, 235, 255);
    static constexpr int  SlateBlue = TNL_ARGB8(106, 90, 205, 255);
    static constexpr int  SlateGray = TNL_ARGB8(112, 128, 144, 255);
    static constexpr int  Snow = TNL_ARGB8(255, 250, 250, 255);
    static constexpr int  SpringGreen = TNL_ARGB8(0, 255, 127, 255);
    static constexpr int  SteelBlue = TNL_ARGB8(70, 130, 180, 255);
    static constexpr int  Tan = TNL_ARGB8(210, 180, 140, 255);
    static constexpr int  Teal = TNL_ARGB8(0, 128, 128, 255);
    static constexpr int  Thistle = TNL_ARGB8(216, 191, 216, 255);
    static constexpr int  Tomato = TNL_ARGB8(255, 99, 71, 255);
    static constexpr int  Turquoise = TNL_ARGB8(64, 224, 208, 255);
    static constexpr int  Violet = TNL_ARGB8(238, 130, 238, 255);
    static constexpr int  Wheat = TNL_ARGB8(245, 222, 179, 255);
    static constexpr int  White = TNL_ARGB8(255, 255, 255, 255);
    static constexpr int  WhiteSmoke = TNL_ARGB8(245, 245, 245, 255);
    static constexpr int  Yellow = TNL_ARGB8(255, 255, 0, 255);
    static constexpr int  YellowGreen = TNL_ARGB8(154, 205, 50, 255);
#endif
    public enum TextColor
    {
        AliceBlue,
        AntiqueWhite,
        Aqua,
        Aquamarine,
        Azure,
        Beige,
        Bisque,
        Black,
        BlanchedAlmond,
        Blue,
        BlueViolet,
        Brown,
        Burlywood,
        CadetBlue,
        Chartreuse,
        Chocolate,
        Coral,
        CornflowerBlue,
        Cornsilk,
        Crimson,
        Cyan,
        DarkBlue,
        DarkCyan,
        DarkGoldenrod,
        DarkGray,
        DarkGreen,
        DarkKhaki,
        DarkMagenta,
        DarkOliveGreen,
        DarkOrange,
        DarkOrchid,
        DarkRed,
        DarkSalmon,
        DarkSeaGreen,
        DarkSlateBlue,
        DarkSlateGray,
        DarkTurquoise,
        DarkViolet,
        DeepPink,
        DeepSkyBlue,
        DimGray,
        DodgerBlue,
        FireBrick,
        FloralWhite,
        ForestGreen,
        Fuchsia,
        Gainsboro,
        GhostWhite,
        Gold,
        Goldenrod,
        Gray,
        Green,
        GreenYellow,
        Honeydew,
        HotPink,
        IndianRed,
        Indigo,
        Ivory,
        Khaki,
        Lavender,
        Lavenderblush,
        LawnGreen,
        LemonChiffon,
        LightBlue,
        LightCoral,
        LightCyan,
        LightGoldenodYellow,
        LightGray,
        LightGreen,
        LightPink,
        LightSalmon,
        LightSeaGreen,
        LightSkyBlue,
        LightSlateGray,
        LightSteelBlue,
        LightYellow,
        Lime,
        LimeGreen,
        Linen,
        Magenta,
        Maroon,
        MediumAquamarine,
        MediumBlue,
        MediumOrchid,
        MediumPurple,
        MediumSeaGreen,
        MediumSlateBlue,
        MediumSpringGreen,
        MediumTurquoise,
        MediumVioletRed,
        MidnightBlue,
        Mintcream,
        MistyRose,
        Moccasin,
        NavajoWhite,
        Navy,
        OldLace,
        Olive,
        Olivedrab,
        Orange,
        Orangered,
        Orchid,
        PaleGoldenrod,
        PaleGreen,
        PaleTurquoise,
        PaleVioletred,
        PapayaWhip,
        PeachPuff,
        Peru,
        Pink,
        Plum,
        PowderBlue,
        Purple,
        Red,
        RosyBrown,
        RoyalBlue,
        SaddleBrown,
        Salmon,
        SandyBrown,
        SeaGreen,
        Seashell,
        Sienna,
        Silver,
        SkyBlue,
        SlateBlue,
        SlateGray,
        Snow,
        SpringGreen,
        SteelBlue,
        Tan,
        Teal,
        Thistle,
        Tomato,
        Turquoise,
        Violet,
        Wheat,
        White,
        WhiteSmoke,
        Yellow,
        YellowGreen,
    }

    public static class DebugExtensions
    {
        public static readonly Dictionary<TextColor, Color32> textColorsDic = new Dictionary<TextColor, Color32>()
        {
            {TextColor.AliceBlue , new Color32(240, 248, 255, 255)},
            {TextColor.AntiqueWhite , new Color32(250, 235, 215, 255)},
            {TextColor.Aqua , new Color32(0, 255, 255, 255)},
            {TextColor.Aquamarine , new Color32(127, 255, 212, 255)},
            {TextColor.Azure , new Color32(240, 255, 255, 255)},
            {TextColor.Beige , new Color32(245, 245, 220, 255)},
            {TextColor.Bisque , new Color32(255, 228, 196, 255)},
            {TextColor.Black , new Color32(0, 0, 0, 255)},
            {TextColor.BlanchedAlmond , new Color32(255, 235, 205, 255)},
            {TextColor.Blue , new Color32(0, 0, 255, 255)},
            {TextColor.BlueViolet , new Color32(138, 43, 226, 255)},
            {TextColor.Brown , new Color32(165, 42, 42, 255)},
            {TextColor.Burlywood , new Color32(222, 184, 135, 255)},
            {TextColor.CadetBlue , new Color32(95, 158, 160, 255)},
            {TextColor.Chartreuse , new Color32(127, 255, 0, 255)},
            {TextColor.Chocolate , new Color32(210, 105, 30, 255)},
            {TextColor.Coral , new Color32(255, 127, 80, 255)},
            {TextColor.CornflowerBlue , new Color32(100, 149, 237, 255)},
            {TextColor.Cornsilk , new Color32(255, 248, 220, 255)},
            {TextColor.Crimson , new Color32(220, 20, 60, 255)},
            {TextColor.Cyan , new Color32(0, 255, 255, 255)},
            {TextColor.DarkBlue , new Color32(0, 0, 139, 255)},
            {TextColor.DarkCyan , new Color32(0, 139, 139, 255)},
            {TextColor.DarkGoldenrod , new Color32(184, 134, 11, 255)},
            {TextColor.DarkGray , new Color32(169, 169, 169, 255)},
            {TextColor.DarkGreen , new Color32(0, 100, 0, 255)},
            {TextColor.DarkKhaki , new Color32(189, 183, 107, 255)},
            {TextColor.DarkMagenta , new Color32(139, 0, 139, 255)},
            {TextColor.DarkOliveGreen , new Color32(85, 107, 47, 255)},
            {TextColor.DarkOrange , new Color32(255, 140, 0, 255)},
            {TextColor.DarkOrchid , new Color32(153, 50, 204, 255)},
            {TextColor.DarkRed , new Color32(139, 0, 0, 255)},
            {TextColor.DarkSalmon , new Color32(233, 150, 122, 255)},
            {TextColor.DarkSeaGreen , new Color32(143, 188, 143, 255)},
            {TextColor.DarkSlateBlue , new Color32(72, 61, 139, 255)},
            {TextColor.DarkSlateGray , new Color32(47, 79, 79, 255)},
            {TextColor.DarkTurquoise , new Color32(0, 206, 209, 255)},
            {TextColor.DarkViolet , new Color32(148, 0, 211, 255)},
            {TextColor.DeepPink , new Color32(255, 20, 147, 255)},
            {TextColor.DeepSkyBlue , new Color32(0, 191, 255, 255)},
            {TextColor.DimGray , new Color32(105, 105, 105, 255)},
            {TextColor.DodgerBlue , new Color32(30, 144, 255, 255)},
            {TextColor.FireBrick , new Color32(178, 34, 34, 255)},
            {TextColor.FloralWhite , new Color32(255, 250, 240, 255)},
            {TextColor.ForestGreen , new Color32(34, 139, 34, 255)},
            {TextColor.Fuchsia , new Color32(255, 0, 255, 255)},
            {TextColor.Gainsboro , new Color32(220, 220, 220, 255)},
            {TextColor.GhostWhite , new Color32(248, 248, 255, 255)},
            {TextColor.Gold , new Color32(255, 215, 0, 255)},
            {TextColor.Goldenrod , new Color32(218, 165, 32, 255)},
            {TextColor.Gray , new Color32(128, 128, 128, 255)},
            {TextColor.Green , new Color32(0, 128, 0, 255)},
            {TextColor.GreenYellow , new Color32(173, 255, 47, 255)},
            {TextColor.Honeydew , new Color32(240, 255, 240, 255)},
            {TextColor.HotPink , new Color32(255, 105, 180, 255)},
            {TextColor.IndianRed , new Color32(205, 92, 92, 255)},
            {TextColor.Indigo , new Color32(75, 0, 130, 255)},
            {TextColor.Ivory , new Color32(255, 255, 240, 255)},
            {TextColor.Khaki , new Color32(240, 230, 140, 255)},
            {TextColor.Lavender , new Color32(230, 230, 250, 255)},
            {TextColor.Lavenderblush , new Color32(255, 240, 245, 255)},
            {TextColor.LawnGreen , new Color32(124, 252, 0, 255)},
            {TextColor.LemonChiffon , new Color32(255, 250, 205, 255)},
            {TextColor.LightBlue , new Color32(173, 216, 230, 255)},
            {TextColor.LightCoral , new Color32(240, 128, 128, 255)},
            {TextColor.LightCyan , new Color32(224, 255, 255, 255)},
            {TextColor.LightGoldenodYellow , new Color32(250, 250, 210, 255)},
            {TextColor.LightGray , new Color32(211, 211, 211, 255)},
            {TextColor.LightGreen , new Color32(144, 238, 144, 255)},
            {TextColor.LightPink , new Color32(255, 182, 193, 255)},
            {TextColor.LightSalmon , new Color32(255, 160, 122, 255)},
            {TextColor.LightSeaGreen , new Color32(32, 178, 170, 255)},
            {TextColor.LightSkyBlue , new Color32(135, 206, 250, 255)},
            {TextColor.LightSlateGray , new Color32(119, 136, 153, 255)},
            {TextColor.LightSteelBlue , new Color32(176, 196, 222, 255)},
            {TextColor.LightYellow , new Color32(255, 255, 224, 255)},
            {TextColor.Lime , new Color32(0, 255, 0, 255)},
            {TextColor.LimeGreen , new Color32(50, 205, 50, 255)},
            {TextColor.Linen , new Color32(250, 240, 230, 255)},
            {TextColor.Magenta , new Color32(255, 0, 255, 255)},
            {TextColor.Maroon , new Color32(128, 0, 0, 255)},
            {TextColor.MediumAquamarine , new Color32(102, 205, 170, 255)},
            {TextColor.MediumBlue , new Color32(0, 0, 205, 255)},
            {TextColor.MediumOrchid , new Color32(186, 85, 211, 255)},
            {TextColor.MediumPurple , new Color32(147, 112, 219, 255)},
            {TextColor.MediumSeaGreen , new Color32(60, 179, 113, 255)},
            {TextColor.MediumSlateBlue , new Color32(123, 104, 238, 255)},
            {TextColor.MediumSpringGreen , new Color32(0, 250, 154, 255)},
            {TextColor.MediumTurquoise , new Color32(72, 209, 204, 255)},
            {TextColor.MediumVioletRed , new Color32(199, 21, 133, 255)},
            {TextColor.MidnightBlue , new Color32(25, 25, 112, 255)},
            {TextColor.Mintcream , new Color32(245, 255, 250, 255)},
            {TextColor.MistyRose , new Color32(255, 228, 225, 255)},
            {TextColor.Moccasin , new Color32(255, 228, 181, 255)},
            {TextColor.NavajoWhite , new Color32(255, 222, 173, 255)},
            {TextColor.Navy , new Color32(0, 0, 128, 255)},
            {TextColor.OldLace , new Color32(253, 245, 230, 255)},
            {TextColor.Olive , new Color32(128, 128, 0, 255)},
            {TextColor.Olivedrab , new Color32(107, 142, 35, 255)},
            {TextColor.Orange , new Color32(255, 165, 0, 255)},
            {TextColor.Orangered , new Color32(255, 69, 0, 255)},
            {TextColor.Orchid , new Color32(218, 112, 214, 255)},
            {TextColor.PaleGoldenrod , new Color32(238, 232, 170, 255)},
            {TextColor.PaleGreen , new Color32(152, 251, 152, 255)},
            {TextColor.PaleTurquoise , new Color32(175, 238, 238, 255)},
            {TextColor.PaleVioletred , new Color32(219, 112, 147, 255)},
            {TextColor.PapayaWhip , new Color32(255, 239, 213, 255)},
            {TextColor.PeachPuff , new Color32(255, 218, 185, 255)},
            {TextColor.Peru , new Color32(205, 133, 63, 255)},
            {TextColor.Pink , new Color32(255, 192, 203, 255)},
            {TextColor.Plum , new Color32(221, 160, 221, 255)},
            {TextColor.PowderBlue , new Color32(176, 224, 230, 255)},
            {TextColor.Purple , new Color32(128, 0, 128, 255)},
            {TextColor.Red , new Color32(255, 0, 0, 255)},
            {TextColor.RosyBrown , new Color32(188, 143, 143, 255)},
            {TextColor.RoyalBlue , new Color32(65, 105, 225, 255)},
            {TextColor.SaddleBrown , new Color32(139, 69, 19, 255)},
            {TextColor.Salmon , new Color32(250, 128, 114, 255)},
            {TextColor.SandyBrown , new Color32(244, 164, 96, 255)},
            {TextColor.SeaGreen , new Color32(46, 139, 87, 255)},
            {TextColor.Seashell , new Color32(255, 245, 238, 255)},
            {TextColor.Sienna , new Color32(160, 82, 45, 255)},
            {TextColor.Silver , new Color32(192, 192, 192, 255)},
            {TextColor.SkyBlue , new Color32(135, 206, 235, 255)},
            {TextColor.SlateBlue , new Color32(106, 90, 205, 255)},
            {TextColor.SlateGray , new Color32(112, 128, 144, 255)},
            {TextColor.Snow , new Color32(255, 250, 250, 255)},
            {TextColor.SpringGreen , new Color32(0, 255, 127, 255)},
            {TextColor.SteelBlue , new Color32(70, 130, 180, 255)},
            {TextColor.Tan , new Color32(210, 180, 140, 255)},
            {TextColor.Teal , new Color32(0, 128, 128, 255)},
            {TextColor.Thistle , new Color32(216, 191, 216, 255)},
            {TextColor.Tomato , new Color32(255, 99, 71, 255)},
            {TextColor.Turquoise , new Color32(64, 224, 208, 255)},
            {TextColor.Violet , new Color32(238, 130, 238, 255)},
            {TextColor.Wheat , new Color32(245, 222, 179, 255)},
            {TextColor.White , new Color32(255, 255, 255, 255)},
            {TextColor.WhiteSmoke , new Color32(245, 245, 245, 255)},
            {TextColor.Yellow , new Color32(255, 255, 0, 255)},
            {TextColor.YellowGreen , new Color32(154, 205, 50, 255)},
        };

        /// <summary>
        /// Debug.Logを行います。
        /// 引数なしの場合はクラス名を表示しません。
        /// </summary>
        public static T Debuglog<T>(this T value, TextColor color = TextColor.White)
        {
            Debug.Log($"{GetColorString(color)} {typeof(T).Name}: {value} {GetColorStringEnd(color)}");
            return value;
        }
        /// <summary>
        /// Debug.Logを行います。
        /// 引数strがある場合、クラス名などは表示せずstr + 値で返します
        /// </summary>
        public static T Debuglog<T>(this T value, string str, TextColor color = TextColor.White)
        {
            Debug.Log($"{str} : {GetColorString(color)}{value}。 {GetColorStringEnd(color)}");
            return value;
        }


        /// <summary>
        /// Debug.LogWarningを行います。
        /// 引数なしの場合はクラス名を表示しません。
        /// </summary>
        public static T DebuglogWarning<T>(this T value, TextColor color = TextColor.Yellow)
        {
            Debug.LogWarning($"{GetColorString(color)} {typeof(T).Name}: {value} {GetColorStringEnd(color)}");
            return value;
        }
        /// <summary>
        /// Debug.LogWarningを行います。
        /// 引数strがある場合、クラス名などは表示せずstr + 値で返します
        /// </summary>
        public static T DebuglogWarning<T>(this T value, string str, TextColor color = TextColor.Yellow)
        {
            Debug.LogWarning($"{str} : {GetColorString(color)}{value}。 {GetColorStringEnd(color)}");
            return value;
        }

        /// <summary>
        /// Debug.LogErrorを行います。
        /// 引数なしの場合はクラス名を表示しません。
        /// </summary>
        public static T DebuglogError<T>(this T value, TextColor color = TextColor.Red)
        {
            Debug.LogError($"{GetColorString(color)} {typeof(T).Name}: {value} {GetColorStringEnd(color)}");
            return value;
        }
        /// <summary>
        /// Debug.LogErrorを行います。
        /// 引数strがある場合、クラス名などは表示せずstr + 値で返します
        /// </summary>
        public static T DebuglogError<T>(this T value, string str, TextColor color = TextColor.Red)
        {
            Debug.LogError($"{str} : {GetColorString(color)}{value}。 {GetColorStringEnd(color)}");
            return value;
        }

        /// <summary>
        /// 色を付けるときの<color = ~~~>を返す
        /// </summary>
        private static string GetColorString(TextColor color)
        {
            return color == TextColor.White ? "" : $"<color=#{ConvertEnumToColorCode(color)}>";
        }
        /// <summary>
        /// TextColorのDictionaryから取得したColorを16進数にして返す。
        /// </summary>
        private static string ConvertEnumToColorCode(TextColor textColor) //https://nekosuko.jp/1674/ のサイトを参考
        {
            return ColorUtility.ToHtmlStringRGBA(textColorsDic[textColor]);
        }
        /// <summary>
        /// 色を付けるときの</color>を返す
        /// </summary>
        private static string GetColorStringEnd(TextColor color)
        {
            return color == TextColor.White ? "" : "</color>";
        }
    }
}