﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Backend.Model
{
    public class Product
    {
        [JsonPropertyName("id")]
        public int ID { get; set; }


        [JsonPropertyName("productName")]
        public string ProductName { get; set; }

        [JsonPropertyName("unitPrice")]
        public decimal UnitPrice { get; set; }
    }

    public class ReceiptLine
    {
        [JsonPropertyName("id")]
        public int ID { get; set; }

        [JsonPropertyName("product")]
        public Product Product { get; set; }

        [JsonPropertyName("amount")]
        public int Amount { get; set; }

        [JsonPropertyName("totalPrice")]
        public decimal TotalPrice { get; set; }
    }

    public class Receipt
    {
        [JsonPropertyName("id")]
        public int ID { get; set; }

        [JsonPropertyName("receiptTimestamp")]
        public DateTime ReceiptTimestamp { get; set; }

        [JsonPropertyName("receiptLines")]
        public List<ReceiptLine> ReceiptLines { get; set; }

        [JsonPropertyName("totalPrice")]
        public decimal TotalPrice { get; set; }
    }
}
