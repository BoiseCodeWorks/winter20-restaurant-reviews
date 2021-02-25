import { AppState } from '../AppState'
import { api } from './AxiosService'

class ProfileService {
  async getProfileById(id) {
    const res = await api.get('api/profiles/' + id)
    console.log(res)
    AppState.searchedProfile = res.data
  }
}
export const profileService = new ProfileService()
