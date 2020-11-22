Vue.component('product-manager', {
    template: `<div>
        <div class="container" v-if="EditMode">
            <label for="ProductName">Name</label>
            <div class="input-group mb-1">
                <input id="ProductName" v-model="productModel.name" type="text" class="form-control" placeholder="Name" aria-label="Name" />
            </div>
            <label for="ProductDescription">Description</label>
            <div class="input-group mb-1">
                <textarea class="form-control" id="ProductDescription" v-model="productModel.description"></textarea>
            </div>
            <label for="ProductPrice">Price</label>
            <div class="input-group">
                <div class="input-group-prepend">
                    <span class="input-group-text" id="priceAddon">RUB</span>
                </div>
                <input id="ProductPrice" class="form-control" v-model.number="productModel.price" aria-describedby="priceAddon" />
            </div>
            <div class="btn-group mt-3">
                <button class="btn btn-outline-info" @@click="updateProduct" v-if="updateMode">Update Product</button>
                <button class="btn btn-outline-info" @@click="createProduct" v-else>Create Product</button>
                <button class="btn btn-outline-warning" @@click="toogleEdit">Cancel</button>
            </div>
        </div>
        <div class="container" v-else>
            <button class="btn btn-info mb-2" @@click="toogleEdit">Add a new Product</button>
            <table class="table table-hover">
                <thead class="thead-dark">
                    <tr>
                        <th>Name</th>
                        <th>Description</th>
                        <th>Price</th>
                        <th></th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="(product, index) in products">
                        <td>
                            {{product.name}}
                        </td>
                        <td>
                            {{product.description}}
                        </td>
                        <td>
                            {{product.price}}
                        </td>
                        <td>
                            <button @@click="editProduct(product, index)" class="btn btn-info">Edit</button>
                        </td>
                        <td>
                            <button @@click="deleteProduct(product.id, index)" class="btn btn-danger">Delete</button>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>`,
    data() {
        return {
            loading: false,
            productModel: {
                id: 0,
                name: "Product Name",
                description: "Product Description",
                price: 10000
            },
            EditMode: false,
            updateMode: false,
            products: [],
            productIndex: 0
        }
        
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
                    this.updateMode = false;
                    this.products.splice(this.productIndex, 1, res.data)
                    console.log(res);
                })
                .catch(err => {
                    alert("Failed to update!");
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });
        },
        editProduct(product, index) {
            this.productIndex = index;
            this.EditMode = true;
            this.updateMode = true;
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
                    alert("Failed to delete!");
                    console.log(err);
                })
                .then(() => {
                    this.products.splice(index, 1);
                    this.loading = false;
                });
        },
        getProduct(id) {
            this.loading = true;

            axios.get("AdminDashboard/GetProductById/" + id)
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
                    alert("Failed to create a new product!");
                    console.log(err);
                })
                .then(() => {
                    this.EditMode = false;
                    this.loading = false;
                });
        },
        toogleEdit() {
            this.productModel = {
                id: 0,
                name: "",
                description: "",
                price: 0,
            };
            this.EditMode = !this.EditMode;
        }
    },
    computed: {

    }
});