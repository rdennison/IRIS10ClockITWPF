using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IrisClockITAttributes.Icons
{
    public enum BootstrapGlyphiconType
    {
        Book = 1,
        Ban,
        CircleArrowLeft,
        CircleArrowRight,
        DownloadAlt,
        Edit,
        Envelope,
        File,
        FileExport,
        Filter,
        Home,
        Image,
        InfoSign,
        ListAlt,
        Lock,
        Login,
        Minus,
        NewWindow,
        OK,
        OKCircle,
        Paperclip,
        Pencil,
        Photo,
        Picture,
        Plus,
        Remove,
        RemoveCircle,
        Save,
        Search,
        Send,
        ShoppingCart,
        Stats,
        StepBackward,
        StepForward,
        Tasks,
        Time,
        Trash,
        USD,
        User,
        Wrench,
        HandRight,
        Asterisk
        
    }

    public static class BootstrapGlyphiconTypeEnumHelper
    {
        public static string GetBoostrapGlyphiconString(BootstrapGlyphiconType glyphiconType)
        {
            var glyphiconTypeString = "";

            switch (glyphiconType)
            {
                case BootstrapGlyphiconType.Asterisk:
                    glyphiconTypeString = "asterisk";
                    break;
                case BootstrapGlyphiconType.Ban:
                    glyphiconTypeString = "ban-circle";
                    break;
                case BootstrapGlyphiconType.Book:
                    glyphiconTypeString = "book";
                    break;
                case BootstrapGlyphiconType.CircleArrowLeft:
                    glyphiconTypeString = "circle-arrow-left";
                    break;
                case BootstrapGlyphiconType.CircleArrowRight:
                    glyphiconTypeString = "circle-arrow-right";
                    break;
                case BootstrapGlyphiconType.DownloadAlt:
                    glyphiconTypeString = "download-alt";
                    break;
                case BootstrapGlyphiconType.Edit:
                    glyphiconTypeString = "edit";
                    break;
                case BootstrapGlyphiconType.Envelope:
                    glyphiconTypeString = "envelope";
                    break;
                case BootstrapGlyphiconType.File:
                    glyphiconTypeString = "file";
                    break;
                case BootstrapGlyphiconType.Filter:
                    glyphiconTypeString = "filter";
                    break;
                case BootstrapGlyphiconType.FileExport:
                    glyphiconTypeString = "download";
                    break;
                case BootstrapGlyphiconType.Home:
                    glyphiconTypeString = "home";
                    break;
                case BootstrapGlyphiconType.Image:
                    glyphiconTypeString = "picture";
                    break;
                case BootstrapGlyphiconType.InfoSign:
                    glyphiconTypeString = "info-sign";
                    break;
                case BootstrapGlyphiconType.ListAlt:
                    glyphiconTypeString = "list-alt";
                    break;
                case BootstrapGlyphiconType.Lock:
                    glyphiconTypeString = "lock";
                    break;
                case BootstrapGlyphiconType.Login:
                    glyphiconTypeString = "log-in";
                    break;
                case BootstrapGlyphiconType.Minus:
                    glyphiconTypeString = "minus";
                    break;
                case BootstrapGlyphiconType.NewWindow:
                    glyphiconTypeString = "new-window";
                    break;
                case BootstrapGlyphiconType.OK:
                    glyphiconTypeString = "ok";
                    break;
                case BootstrapGlyphiconType.OKCircle:
                    glyphiconTypeString = "ok-circle";
                    break;
                case BootstrapGlyphiconType.Paperclip:
                    glyphiconTypeString = "paperclip";
                    break;
                case BootstrapGlyphiconType.Pencil:
                    glyphiconTypeString = "pencil";
                    break;
                case BootstrapGlyphiconType.Photo:
                case BootstrapGlyphiconType.Picture:
                    glyphiconTypeString = "picture";
                    break;
                case BootstrapGlyphiconType.Plus:
                    glyphiconTypeString = "plus";
                    break;
                case BootstrapGlyphiconType.Remove:
                    glyphiconTypeString = "remove";
                    break;
                case BootstrapGlyphiconType.RemoveCircle:
                    glyphiconTypeString = "remove-circle";
                    break;
                case BootstrapGlyphiconType.Save:
                    glyphiconTypeString = "save";
                    break;
                case BootstrapGlyphiconType.Search:
                    glyphiconTypeString = "search";
                    break;
                case BootstrapGlyphiconType.Send:
                    glyphiconTypeString = "send";
                    break;
                case BootstrapGlyphiconType.ShoppingCart:
                    glyphiconTypeString = "shopping-cart";
                    break;
                case BootstrapGlyphiconType.Stats:
                    glyphiconTypeString = "stats";
                    break;
                case BootstrapGlyphiconType.StepBackward:
                    glyphiconTypeString = "step-backward";
                    break;
                case BootstrapGlyphiconType.StepForward:
                    glyphiconTypeString = "step-forward";
                    break;
                case BootstrapGlyphiconType.Tasks:
                    glyphiconTypeString = "tasks";
                    break;
                case BootstrapGlyphiconType.Time:
                    glyphiconTypeString = "time";
                    break;
                case BootstrapGlyphiconType.Trash:
                    glyphiconTypeString = "trash";
                    break;
                case BootstrapGlyphiconType.USD:
                    glyphiconTypeString = "usd";
                    break;
                case BootstrapGlyphiconType.User:
                    glyphiconTypeString = "user";
                    break;
                case BootstrapGlyphiconType.Wrench:
                    glyphiconTypeString = "wrench";
                    break;
                case BootstrapGlyphiconType.HandRight:
                    glyphiconTypeString = "hand-right";
                    break;
                default:
                    throw new ApplicationException("Unexpected BootstrapGlyphiconType enum value: " + glyphiconType.ToString());
            }

            return "glyphicon-" + glyphiconTypeString;
        }
    }
}