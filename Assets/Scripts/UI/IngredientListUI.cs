using General;
using UnityEngine;

namespace UI
{
    public class IngredientListUI : MonoBehaviour
    {
        [SerializeField] private GameObject ingredientListProvider;
        
        
        private IIngredientListProvider m_IngredientListProvider;
        private IconUI[] m_Icons;


        private void Awake()
        {
            m_IngredientListProvider = ingredientListProvider.GetComponent<IIngredientListProvider>();

            if (m_IngredientListProvider == null)
            {
                Debug.LogError($"{ingredientListProvider} does not implement {nameof(IIngredientListProvider)}!");
            }

            m_Icons = GetComponentsInChildren<IconUI>(true);
            m_IngredientListProvider.OnIngredientListChanged += OnIngredientListChanged;
        }

        private void OnDestroy()
        {
            m_IngredientListProvider.OnIngredientListChanged += OnIngredientListChanged;
        }


        private void OnIngredientListChanged(IngredientListChangedArgs args)
        {
            for (var i = 0; i < m_Icons.Length; i++)
            {
                if (i >= args.ingredients.Length)
                {
                    m_Icons[i].gameObject.SetActive(false);
                    continue;
                }

                if (args.ingredients[i] == null)
                {
                    m_Icons[i].gameObject.SetActive(false);
                    continue;
                }
                
                m_Icons[i].Sprite = args.ingredients[i].Icon;
                m_Icons[i].gameObject.SetActive(true);
            }
        }
    }
}