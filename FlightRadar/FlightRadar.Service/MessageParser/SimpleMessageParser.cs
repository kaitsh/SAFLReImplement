﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FlightRadar.Model;

namespace FlightRadar.Service.MessageParser
{
    class SimpleMessageParser : IMessageParser
    {
        /// <summary>
        /// parse String from JSON ADSB Sentence
        /// </summary>
        /// <param name="adsbSentence"></param>
        /// <returns></returns>
        public string Parse(string adsbSentence)
        {
            Regex pattern = new Regex(@"\d+\.\d+!ADS-B\*[0-9A-Z]{28}");
            string myString = pattern.Match(adsbSentence).ToString();

            return myString;
        }

        public string ParseTimestamp(string sentence)
        {
            return sentence.Substring(0, 18);
        }

        public string ParseDfca(string sentence)
        {
            return sentence.Substring(25, 2);
        }

        public string ParseIcao(string sentence)
        {
            return sentence.Substring(27, 6);
        }

        public string ParsePayload(string sentence)
        {
            return sentence.Substring(33, 14);
        }

        public string ParsePartiy(string sentence)
        {
            return sentence.Substring(47);
        }

        public ADSBMessagetype ParseMessagetype(string payloadInBin)
        {
            int typeCode = Convert.ToInt32(payloadInBin.Substring(0, 4));

            if (typeCode == 0 || (typeCode >= 9 && typeCode <= 18) || (typeCode >= 20 && typeCode <= 22))
                return ADSBMessagetype.Position;
            if (typeCode >= 1 && typeCode <= 4)
                return ADSBMessagetype.Identification;
            if (typeCode == 19)
                return ADSBMessagetype.Velocity;

            return ADSBMessagetype.undefined;
        }

    }
}