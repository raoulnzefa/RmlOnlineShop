Vue.config.devtools = true;

var app = new Vue(
    {
        el: '#app',
        data: {
            loading: false,
            getProductsButtonPressed: false,
            productModel: {
                id: 0,
                name: "Product Name",
                description: "Product Description",
                price: 10000
            },
            EditMode: false,
            products: [],
            productIndex: 0,
        },
        mounted() {
            this.getProducts();
        },
        methods: {
            updateProduct() {
                this.loading = true;

                axios.put("AdminDashboard/UpdateProduct", this.productModel)
                    .then(res => {
                        this.EditMode = false;
                        this.products.splice(this.productIndex, 1, res.data)
                        console.log(res);
                    })
                    .catch(err => {
                        console.log(err);
                    })
                    .then(() => {
                        this.loading = false;
                    });
            },
            editProduct(product, index) {
                this.productIndex = index;
                this.EditMode = true;
                this.productModel = {
                    id: product.id,
                    name: product.name,
                    description: product.description,
                    price: product.price,
                }
            },
            deleteProduct(id, index) {
                this.loading = true;
                
                axios.delete("AdminDashboard/DeleteProduct/" + id)
                    .then(res => {
                        console.log(res);
                    })
                    .catch(err => {
                        console.log(err);
                    })
                    .then(() => {
                        this.products.splice(index, 1);
                        this.loading = false;
                    });
            },
            getProduct(id) {
                this.loading = true;

                axios.get("AdminDashboard/GetProductById" + id)
                    .then(res => {
                        
                        console.log(res);
                    })
                    .catch(err => {
                        console.log(err);
                    })
                    .then(() => {
                        this.loading = false;
                    });
            },
            getProducts() {
                this.loading = true;
                axios.get("AdminDashboard/GetAllProducts")
                    .then(res => {
                        this.products = res.data;
                        this.getProductsButtonPressed = true;
                        console.log(res);
                    })
                    .catch(err => {
                        console.log(err);
                    })
                    .then(() => {
                        this.loading = false;
                    });
            },
            createProduct() {
                this.loading = true;
                axios.post('AdminDashboard/CreateProduct', this.productModel)
                    .then(res => {
                        this.products.push(res.data);
                        console.log(res);
                    })
                    .catch(err => {
                        console.log(err);
                    })
                    .then(() => {
                        this.loading = false;
                    });
            },
            toogleEdit() {
                this.EditMode = !this.EditMode;
            }
        },
        computed: {
           
        }
    }
);