<template>
  <div class="container">
    <div class="row">
      <div class="col-md-4 my-2 order-md-0 order-1">
        <span class="text-small text-secondary">&OpenCurlyQuote;Cook up something special with our
          recipes.&CloseCurlyQuote;</span>
      </div>
      <div class="col-md-4 text-center order-md-1 order-0 text-small text-secondary">

        <div class="row">
          <div class="col-4"><span class="selectable rounded px-2 pb-1">All Recipes</span></div>
          <div class="col-4"><span class="selectable rounded px-2 pb-1">My Recipes</span></div>
          <div class="col-4"><span class="selectable rounded px-2 pb-1">Favorites</span></div>
        </div>

      </div>
    </div>

    <!-- SECTION - Recipe Card -->
    <div class="row">
      <div class="col-12 col-md-6 col-lg-4 col-xl-3 my-2" v-for="r in recipes" :key="r.id">
        <div class="imgContainer pt-1 elevation-3" :style="{ 'background-image': 'url(' + r.img + ')' }" :title="r.title">

          <div class="row justify-content-between">
            <div class="col-9">
              <div class="recipeGlass text-center m-2">
                <h6 class="text-dark">{{ r.category }}</h6>
              </div>
            </div>
            <div class="col-3">
              <div class="recipeGlass text-center m-2 selectable" @click="changeFavorite(r.id)">
                <span class="mdi mdi-heart-outline"></span>
              </div>
            </div>
          </div>

          <div class="row setHeight">
            <div class="col-12 titleContainer">
              <div :class="r.creatorId != account.id ? 'recipeGlass m-2 px-3' : 'myRecipeGlass m-2 px-3'">
                <h6>{{ r.title.substring(0, 25) }} {{ r.title[26] ? '...' : '' }}</h6>
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
import { logger } from "../utils/Logger.js";

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
      recipes: computed(() => AppState.recipes),
      account: computed(() => AppState.account),

      changeFavorite(recipeId) {
        logger.log("changing favorite " + recipeId)
      }
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
  backdrop-filter: blur(4px);
  -webkit-backdrop-filter: blur(4px);
  border-radius: 500px;
}

.myRecipeGlass {
  /* From https://css.glass */
  background: rgba(67, 29, 238, 0.5);
  backdrop-filter: blur(4px);
  -webkit-backdrop-filter: blur(4px);
  border-radius: 500px;
  color: white;
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
  margin-top: 16vh;
  width: auto;
}

.setHeight {
  height: 20vh;
}
</style>
