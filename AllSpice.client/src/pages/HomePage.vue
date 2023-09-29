<template>
  <div class="container">
    <div class="row mb-2">
      <!-- TAG LINE -->
      <div class="col-md-4 my-2 order-md-0 order-1">
        <span class="text-small text-secondary">Cook up something special with our
          recipes.</span>
      </div>
      <!-- FILTERS -->
      <div
        class="col-md-4 text-center order-md-1 order-0 text-small text-secondary elevation-3 bg-light rounded pb-1 mb-0 pt-1">

        <div class="row">
          <div class="col-4"><span class="selectable rounded px-2 pb-1" @click="getAllRecipes()">All Recipes</span></div>
          <div class="col-4"><span class="selectable rounded px-2 pb-1" @click="getAllMyRecipes()">My Recipes</span></div>
          <div class="col-4"><span class="selectable rounded px-2 pb-1" @click="getAllMyFavorites()">Favorites</span>
          </div>
        </div>

      </div>

      <!-- TAG LINE 2 -->
      <div class="col-md-4 my-2 order-md-3 order-3 text-end">
        <span class="text-small text-secondary">...or just do take-out.</span>
      </div>
    </div>

    <!-- SECTION - START Recipe Card -->
    <div class="row">
      <div class="col-12 col-md-6 col-lg-4 col-xl-3 my-2" v-for="r in recipes" :key="r.id">
        <div class="imgContainer pt-1 elevation-3" :style="{ 'background-image': 'url(' + r.img + ')' }">

          <!-- CATEGORY -->
          <div class="row justify-content-between">
            <div class="col-9">
              <div class="recipeGlass text-center m-2">
                <h6 class="text-dark">{{ r.category }}</h6>
              </div>
            </div>

            <!-- FAVORITE -->
            <div class="col-3">
              <div class="recipeGlass text-center m-2 selectable" @click="changeFavorite(r.id)">
                <h6><i v-if="r.id == favorites.find(f => f.id == r.id)?.id" class="mdi mdi-heart text-danger"></i>
                  <i v-else class="mdi mdi-heart-outline"></i>
                </h6>
              </div>
            </div>
          </div>

          <!-- TITLE -->
          <div class="row setHeight">
            <div class="col-12 titleContainer">
              <div :class="r.creatorId != account.id ? 'recipeGlass m-2 px-3' : 'myRecipeGlass m-2 px-3'">
                <h6 :title="r.creatorId != account.id ? r.title : 'Your recipe for: ' + r.title">{{ r.title.substring(0,
                  25) }} {{ r.title[26] ? '...' : '' }}</h6>
              </div>
            </div>
          </div>

        </div>
      </div>
    </div>
    <!-- SECTION - END Recipe Card -->

  </div>
</template>

<script>
import { computed, onMounted } from "vue";
import Pop from "../utils/Pop.js";
import { recipesService } from "../services/RecipesService.js";
import { favoritesService } from "../services/FavoritesService.js";
import { AppState } from "../AppState.js";
import { logger } from "../utils/Logger.js";
import { accountService } from "../services/AccountService.js";

export default {
  setup() {
    onMounted(() => {
      getRecipes()
    })
    async function getRecipes() {
      try {
        await recipesService.getRecipes()
        await getMyFavorites()
      } catch (error) {
        Pop.error(error)
      }
    }
    async function getMyFavorites() {
      try {
        await accountService.getMyFavorites()
      } catch (error) {
        Pop.error(error)
      }
    }
    return {
      recipes: computed(() => AppState.recipes),
      account: computed(() => AppState.account),
      favorites: computed(() => AppState.favorites),

      changeFavorite(recipeId) {
        // logger.log("changing favorite " + recipeId)
        if (recipeId == AppState.favorites.find(f => f.id == recipeId)?.id) {
          let favoriteId = AppState.favorites.find(f => f.id == recipeId)?.favoriteId
          // logger.log("Delete Favorite.", favoriteId)
          favoritesService.deleteFavorite(favoriteId)
        } else {
          // logger.log("Add Favorite", recipeId)
          favoritesService.createFavorite(recipeId)
        }
        getRecipes() // FIXME I definitely shouldn't have to do this.
      },

      getAllRecipes() {
        getRecipes()
      },

      getAllMyRecipes() {
        logger.log("getAllMyRecipes", AppState.recipes.length, AppState.myRecipes.length)
        AppState.myRecipes.length = 0
        getRecipes()
        AppState.recipes.forEach(r => {
          if (r.creatorId == AppState.account.id) {
            logger.log("myRecipe", r.id)
            AppState.myRecipes.push(r)
          }
        })
        logger.log("myRecipes ", AppState.myRecipes)
        AppState.recipes.length = 0
        AppState.myRecipes.forEach(m => AppState.recipes.push(m))
        // AppState.recipes.forEach((r) => {
        //   logger.log(r.creatorId, ' Creator - Account ', AppState.account.id, ' Recipe ', r)
        //   if (r.creatorId == AppState.account.id) {
        //     logger.log("New myRecipe found: ", r)
        //     AppState.myRecipes.push(r)
        //   } else logger.log('no match', r)
        //   AppState.recipes.length = 0
        //   AppState.myRecipes.forEach(m => AppState.recipes.push(m))
        // })
      },

      getAllMyFavorites() {
        AppState.recipes.length = 0
        AppState.favorites.forEach(f => AppState.recipes.push(f))
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
  background: rgba(195, 160, 64, 0.5);
  backdrop-filter: blur(4px);
  -webkit-backdrop-filter: blur(4px);
  border-radius: 500px;
  // color: white;
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
