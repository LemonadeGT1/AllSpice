import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"


class FavoritesService {
  async createFavorite(recipeId) {
    const res = await api.post('api/favorites', { recipeId })
    logger.log('createFavorite', res.data)
    AppState.favorites.push(res.data)
    logger.log("createFavorite AppState.favorites", AppState.favorites)
  }

  async deleteFavorite(favoriteId) {
    const res = await api.delete('api/favorites/' + favoriteId)
    logger.log("Removing Favorite.", res.data)
    const favIndex = AppState.favorites.findIndex(f => f.favoriteId == favoriteId)
    AppState.favorites.splice(favIndex, 1)
    logger.log("deleteFavorite AppState.favorites", AppState.favorites)
  }

}


export const favoritesService = new FavoritesService()