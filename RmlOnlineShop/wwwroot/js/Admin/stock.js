Vue.config.devtools = true;

var app = new Vue({
    el: "#stockComponent",
    data: {
        loading: false,
        products: [],
        selectedProduct: null,
        editMode: false,
        editNewStock: false,
        editStockMode: false,
        stockCurrentIndex: 0,
        stockModel: {
            id:0,
            description: "",
            quantity: 0,
            productId: 0,
        },

    },
    mounted() {
        this.getStocks();
    },
    methods: {
        getStocks() {
            this.loading = true;
            axios.get("GetAllStocks")
                .then(res => {
                    this.products = res.data;
                    console.log(res);
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });
        },
        manageProduct(product) {
            this.editMode = true;
            this.selectedProduct= product;
            
        },
        createStock(productId) {
            this.loading = true;
            this.stockModel.productId = productId;
            axios.post("CreateStock", this.stockModel)
                .then(res => {
                    this.selectedProduct.stocksViewModel.push(res.data);
                    this.editNewStock = false;
                    this.editMode = true;
                    console.log(res.data);
                })
                .catch(err => {
                    alert("Failed to create a new stock!");
                    console.error(err);
                })
                .then(() => {
                    this.loading = false;
                });
        },

        deleteStock(id, index) {
            this.loading = true;
            axios.delete("DeleteStock?id=" + id)
                .then(res => {
                    this.selectedProduct.stocksViewModel.splice(index, 1);
                })
                .catch(err => {
                    alert("Something went wrong!");
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });
        },

        updateStock() {
            this.loading = true;
            axios.put("UpdateStock", this.stockModel)
                .then(res => {
                    this.editStockMode = false;
                })
                .catch(err => {
                    alert("Failed to save changes!")
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });
        },

        toogleEditingNewStock() {
            this.toogleEdit();
            this.editNewStock = !this.editNewStock;
        },

        toogleEdit() {
            this.editMode = !this.editMode;
        },
        toogleUpdateStock(stock,index) {
            this.stockModel = stock;
            this.stockCurrentIndex = index;
            this.editStockMode = !this.editStockMode;
        },
        cancelUpdateStock() {
            this.editStockMode = !this.editStockMode;
        }
    }
});
