<template>
  <div class="container">
    <div class="row">
      <div class="col-md-4 my-2 order-md-0 order-1">
        <span class="text-small text-secondary">"Cook up something special with our recipes."</span>
      </div>
      <div class="col-md-4 text-center order-md-1 order-0 text-small text-secondary">
        <span>Home</span>
        <span>My Recipes</span>
        <span>Favorites</span>
      </div>
    </div>
    <div class="row">
      <div class="col-12 col-md-6 col-lg-4 col-xl-3 my-2" v-for="r in recipes" :key="r.id">
        <div class="imgContainer pt-1" :style="{ 'background-image': 'url(' + r.img + ')' }">

          <div class="row justify-content-between">
            <div class="col-6">
              <div class="recipeGlass text-center m-2">
                <h5 class="text-dark">{{ r.category }}</h5>
              </div>
            </div>
            <div class="col-3">
              <div class="recipeGlass text-center m-2 selectable">
                <span class="mdi mdi-heart-outline"></span>
              </div>
            </div>
          </div>

          <div class="row setHeight">
            <div class="col-12 titleContainer">
              <div class="recipeGlass m-2 px-3">
                <h5>{{ r.title.substring(0, 22) + '...' }}</h5>
              </div>
            </div>
          </div>

        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from "vue";
import Pop from "../utils/Pop.js";
import { recipesService } from "../services/RecipesService.js";
import { AppState } from "../AppState.js";

export default {
  setup() {
    onMounted(() => {
      getRecipes()
    })
    async function getRecipes() {
      try {
        await recipesService.getRecipes()
      } catch (error) {
        Pop.error(error)
      }
    }
    return {
      recipes: computed(() => AppState.recipes)
    };
  },
};
</script>

<style scoped lang="scss">
.home {
  display: grid;
  height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;

  .home-card {
    width: 50vw;

    >img {
      height: 200px;
      max-width: 200px;
      width: 100%;
      object-fit: contain;
      object-position: center;
    }
  }
}

.recipeGlass {
  /* From https://css.glass */
  background: rgba(234, 239, 227, 0.5);
  // box-shadow: 0 4px 5px rgba(0, 0, 0, 0.1);
  backdrop-filter: blur(4px);
  -webkit-backdrop-filter: blur(4px);
  // color: #000000;
  // padding: 10px 10px 1px 10px;
  // height: 12vh;
  border-radius: 500px;
  // width: 30%;
}

.imgContainer {
  // height: 20vh;
  // width: auto;
  background-position: center;
  background-size: cover;
  border-radius: 15px;
}

.titleContainer {
  // height: 20vh;
  margin-top: 15vh;
  width: auto;
}

.setHeight {
  height: 20vh;
}
</style>
