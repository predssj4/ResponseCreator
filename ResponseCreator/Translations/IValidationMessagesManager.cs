using System;
using System.Collections.Generic;
using System.Text;

namespace ResponseCreator.Translations
{
    public interface IValidationMessagesManager
    {
        /// <summary>
        /// Provides validation message for given key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetValidationMessageByKey(string key);

        /// <summary>
        /// Provides validation messages for given key and replaces some parts of message
        /// </summary>
        /// <param name="key"></param>
        /// <param name="paramsToInsertIntoMessage">Params that will be inserted into message. The order does matter.</param>
        /// <returns></returns>
        string GetValidationMessageByKey(string key, params object[] paramsToInsertIntoMessage);
    }
}
